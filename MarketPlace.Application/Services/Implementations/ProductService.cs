using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.DataLayer.Entities.Products;
using MarketPlace.DataLayer.Repository;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.DataLayer.DTOs.Common;

namespace MarketPlace.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region constructor

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;
        private readonly IGenericRepository<ProductColor> _productColorRepository;
        private readonly IGenericRepository<ProductGallery> _productGalleryRepository;
        private readonly IGenericRepository<ProductFeature> _productFeatureRepository;
        private readonly IGenericRepository<ProductDiscount> _productDiscountRepository;

        public ProductService(IGenericRepository<Product> productRepository, 
            IGenericRepository<ProductCategory> productCategoryRepository, 
            IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository,
            IGenericRepository<ProductColor> productColorRepository,
            IGenericRepository<ProductGallery> productGalleryRepository,
            IGenericRepository<ProductFeature> productFeatureRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _productColorRepository = productColorRepository;
            _productGalleryRepository = productGalleryRepository;
            _productFeatureRepository = productFeatureRepository;
        }

        #endregion

        #region product

        public async Task<FilterProductDTO> FilterProduct(FilterProductDTO filter)
        {
            var query = _productRepository.GetQuery()
                .Include(s => s.ProductSelectedCategories)
                .ThenInclude(s => s.ProductCategory)
                .AsQueryable();

            var expensiveProduct = await query.OrderByDescending(s => s.Price).FirstOrDefaultAsync();
            filter.FilterMaxPrice = expensiveProduct.Price;

            #region state

            switch (filter.FilterProductState)
            {
                case FilterProductState.All:
                    break;

                case FilterProductState.Active:
                    query = query.Where(p => p.ProductAcceptanceState == ProductAcceptanceState.Accepted && p.IsActive);
                    break;

                case FilterProductState.NotActive:
                    query = query.Where(p => p.ProductAcceptanceState == ProductAcceptanceState.Accepted && !p.IsActive);
                    break;

                case FilterProductState.Accepted:
                    query = query.Where(p => p.ProductAcceptanceState == ProductAcceptanceState.Accepted );
                    break;

                case FilterProductState.Rejected:
                    query = query.Where(p => p.ProductAcceptanceState == ProductAcceptanceState.Rejected);
                    break;

                case FilterProductState.UnderProgress:
                    query = query.Where(p => p.ProductAcceptanceState == ProductAcceptanceState.UnderProgress);
                    break;
            }

            switch (filter.FilterProductOrderBy)
            {
                case FilterProductOrderBy.CreateData_Des:
                    query = query.OrderByDescending(p => p.CreateDate);
                    break;

                case FilterProductOrderBy.CreateDate_Asc:
                    query = query.OrderBy(p => p.CreateDate);
                    break;

                case FilterProductOrderBy.Price_Des:
                    query = query.OrderByDescending(p => p.Price);
                    break;

                case FilterProductOrderBy.Price_Asc:
                    query = query.OrderBy(p => p.Price);
                    break;
            }

            #endregion

            #region filter

            if (!string.IsNullOrEmpty(filter.ProductTitle))
                query = query.Where(p => EF.Functions.Like(p.Title, $"%{filter.ProductTitle}%"));

            if (filter.SellerId != null && filter.SellerId != 0)
                query = query.Where(p => p.SellerId == filter.SellerId.Value);

            if (filter.SelectedMaxPrice == 0) filter.SelectedMaxPrice = expensiveProduct.Price;

            query = query.Where(p => p.Price >= filter.SelectedMinPrice);
            query = query.Where(p => p.Price <= filter.SelectedMaxPrice);

            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(p => p.ProductSelectedCategories.Any(c => c.ProductCategory.UrlName == filter.Category));

            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetProducts(allEntities).SetPaging(pager);
        }

        public async Task<CreateProductResult> CreateProduct(CreateProductDTO product, long sellerId, IFormFile productImage)
        {
            if (productImage == null) return CreateProductResult.HasNoImage;

            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productImage.FileName);

            var res = productImage.AddImageToServer(imageName, PathExtension.ProductImageServer, 150, 150, PathExtension.ProductThumbnailImageImageServer);

            if (res)
            {
                //create product
                var newProduct = new Product
                {
                    Title = product.Title,
                    Price = product.Price,
                    Description = product.Description,
                    ShortDescription = product.ShortDescription,
                    IsActive = product.IsActive,
                    SellerId = sellerId,
                    ImageName = imageName, 
                    ProductAcceptanceState = ProductAcceptanceState.UnderProgress,
                };

                await _productRepository.AddEntity(newProduct);
                await _productRepository.SaveChanges();

                // create product categories
                await AddProductSelectedCategories(newProduct.Id, product.SelectedCategories);
                await _productSelectedCategoryRepository.SaveChanges();

                //create product colors
                await AddProductSelectedColors(newProduct.Id, product.ProductColors);
                await _productColorRepository.SaveChanges();

                //create product features
                await CreateProductFeatures(newProduct.Id, product.ProductFeatures);

                return CreateProductResult.Success;
            }
                return CreateProductResult.Error;
        }

        public async Task<bool> AcceptSellerProduct(long productId)
        {
            var product = await _productRepository.GetEntityById(productId);
            if (product != null)
            {
                product.ProductAcceptanceState = ProductAcceptanceState.Accepted;
                product.ProductAcceptOrRejectDescription = 
                    $"محصول مورد نظر در تاریخ {DateTime.Now.ToShamsi()} مورد تایید سایت قرار گرفت";

                _productRepository.EditEntity(product);
                await _productRepository.SaveChanges();

                return true;
            }
            return false;
        }

        public async Task<bool> RejectSellerProduct(RejectItemDTO reject)
        {
            var product = await _productRepository.GetEntityById(reject.Id);

            if (product != null)
            {
                product.ProductAcceptanceState = ProductAcceptanceState.Rejected;
                product.ProductAcceptOrRejectDescription = reject.RejectMessage;

                _productRepository.EditEntity(product);
                await _productRepository.SaveChanges();

                return true;
            }
            return false;
        }

        public async Task<EditProductDTO> GetProductForEdit(long productId)
        {
            var product = await _productRepository.GetEntityById(productId);

            if (product == null) return null;

            return new EditProductDTO
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                IsActive = product.IsActive,
                ImageName = product.ImageName,

                ProductColors = await _productColorRepository
                .GetQuery().AsQueryable()
                .Where(c => !c.IsDelete && c.ProductId == product.Id)
                .Select(c => new CreateProductColorDTO 
                { 
                    Price = c.Price, 
                    ColorName = c.ColorName,
                    ColorCode = c.ColorCode 
                }).ToListAsync(),

                SelectedCategories = await _productSelectedCategoryRepository.GetQuery().AsQueryable()
                .Where(c => c.ProductId == productId).Select(c => c.ProductCategoryId).ToListAsync(),

                ProductFeatures = await _productFeatureRepository.GetQuery().AsQueryable()
                .Where(f => !f.IsDelete && f.ProductId == product.Id)
                .Select(f => new CreateProductFeatureDTO 
                {
                    Feature = f.FeatureTitle,
                    FeatureValue = f.FeatureValue 
                }).ToListAsync()
            };
        }

        public async Task<EditProductResult> EditSellerProduct(EditProductDTO product, long userId, IFormFile productImage)
        {
            var mainProduct = await _productRepository.GetQuery().AsQueryable()
                .Include(p => p.Seller)
                .SingleOrDefaultAsync(p => p.Id == product.Id);

            if (mainProduct == null) return EditProductResult.NotFound;
            if (mainProduct.Seller.UserId != userId) return EditProductResult.NotForUser;

            mainProduct.Title = product.Title;
            mainProduct.Description = product.Description;
            mainProduct.ShortDescription = product.ShortDescription;
            mainProduct.Price = product.Price;
            mainProduct.IsActive = product.IsActive;
            mainProduct.ProductAcceptanceState = ProductAcceptanceState.UnderProgress;

            if (productImage != null)
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productImage.FileName);

                var res = productImage.AddImageToServer(imageName, PathExtension.ProductImageServer, 150, 150, PathExtension.ProductThumbnailImageImageServer , mainProduct.ImageName);

                if(res)
                {
                    mainProduct.ImageName = imageName;
                }
            }
            _productRepository.EditEntity(mainProduct);
            await _productGalleryRepository.SaveChanges();

            //remove all product categories
            await RemoveAllProductSelectedCategories(product.Id);
            await AddProductSelectedCategories(product.Id, product.SelectedCategories);
            await _productSelectedCategoryRepository.SaveChanges();

            //remove all product color
            await RemoveAllProductSelectedColors(product.Id);
            await AddProductSelectedColors(product.Id, product.ProductColors);
            await _productColorRepository.SaveChanges();

            //remove all product Feature
            await RemoveAllProductFeatures(product.Id);
            await CreateProductFeatures(product.Id, product.ProductFeatures);

            return EditProductResult.Success;
        }

        public async Task RemoveAllProductSelectedCategories(long productId)
        {
            _productSelectedCategoryRepository.DeletePermanentEntities(await _productSelectedCategoryRepository.GetQuery()
                .AsQueryable().Where(s => s.ProductId == productId).ToListAsync());
        }

        public async Task RemoveAllProductSelectedColors(long productId)
        {
            _productColorRepository.DeletePermanentEntities(await _productColorRepository.GetQuery().AsQueryable()
                .Where(s => s.ProductId == productId).ToListAsync());
        }

        public async Task AddProductSelectedColors(long productId, List<CreateProductColorDTO> colors)
        {
            if (colors != null && colors.Any())
            {
                var productSelectedColors = new List<ProductColor>();

                foreach (var productColor in colors)
                {
                    if (productSelectedColors.Any(c => c.ColorName != productColor.ColorName))
                    {
                        productSelectedColors.Add(new ProductColor
                        {
                            ColorName = productColor.ColorName,
                            Price = productColor.Price,
                            ProductId = productId,
                            ColorCode = productColor.ColorCode
                        });
                    }
                }

                await _productColorRepository.AddRangeEntities(productSelectedColors);
            }
        }

        public async Task AddProductSelectedCategories(long productId, List<long> SelectedCategories)
        {
            if (SelectedCategories != null && SelectedCategories.Any())
            {
                var productSelectedCategories = new List<ProductSelectedCategory>();

                foreach (var categoryId in SelectedCategories)
                {
                    productSelectedCategories.Add(new ProductSelectedCategory
                    {
                        ProductCategoryId = categoryId,
                        ProductId = productId
                    });
                }
                await _productSelectedCategoryRepository.AddRangeEntities(productSelectedCategories);
            }
        }

        public async Task<ProductDetailDTO> GetProductDetailById(long productId)
        {
            var product = await _productRepository.GetQuery().AsQueryable()
                .Include(p => p.Seller).ThenInclude(s => s.User)
                .Include(p => p.ProductSelectedCategories).ThenInclude(p => p.ProductCategory)
                .Include(p => p.ProductGalleries)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductFeatures)
                .SingleOrDefaultAsync(p => p.Id == productId);

            if (product == null) return null;

            var SelectedCategoriesIds = product.ProductSelectedCategories.Select(c => c.ProductCategoryId).ToList();

            return new ProductDetailDTO
            {
                ProductId = productId,
                Title = product.Title,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                ImageName = product.ImageName,
                Seller = product.Seller,

                ProductCategories = product.ProductSelectedCategories.Select(s => s.ProductCategory).ToList(),

                ProductGalleries = product.ProductGalleries.ToList(),

                ProductColors = product.ProductColors.ToList(),

                ProductFeatures = product.ProductFeatures.ToList(),

                SellerId = product.SellerId,

                RelatedProducts = await _productRepository.GetQuery()
                .Include(p => p.ProductSelectedCategories)
                .Where(r => r.ProductSelectedCategories.Any(c => SelectedCategoriesIds.Contains(c.ProductCategoryId)) && r.Id != productId 
                && r.ProductAcceptanceState == ProductAcceptanceState.Accepted).ToListAsync(),
            };
        }

        public async Task<List<Product>> FilterProductsForSellerByProductName(string ProductName, long sellerId)
        {
            return await _productRepository.GetQuery().AsQueryable()
                .Where(p => EF.Functions.Like(p.Title , $"%{ProductName}%") && p.SellerId == sellerId).ToListAsync();
        }

        public async Task<List<ProductDiscount>> GetAllOffProducts(int take)
        {
            return await _productDiscountRepository.GetQuery().AsQueryable()
                .Include(s => s.Product)
                .Where(s => s.ExpireDate >= DateTime.Now)
                .OrderByDescending(s => s.ExpireDate)
                .Skip(0)
                .Take(take)
                .Distinct()
                .ToListAsync();
        }

        #endregion


        #region product categories

        public async Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId)
        {
            if(parentId == null || parentId == 0)
            {
                return await _productCategoryRepository.GetQuery().AsQueryable()
                    .Where(p => !p.IsDelete && p.IsActive && p.ParentId == null).ToListAsync();
            }
            return await _productCategoryRepository.GetQuery().AsQueryable()
                .Where(p => !p.IsDelete && p.IsActive && p.ParentId == parentId).ToListAsync();
        }

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await _productCategoryRepository.GetQuery().AsQueryable()
                .Where(s => s.IsActive && !s.IsDelete).ToListAsync();
        }

        #endregion

        public async Task<List<Product>> GetCategorysProductsByCategoryName(string categoryName, int count = 12)
        {
            var category = await _productCategoryRepository.GetQuery().SingleOrDefaultAsync(s => s.UrlName == categoryName);

            if (category == null) return null;

            return await _productSelectedCategoryRepository.GetQuery()
                .AsQueryable()
                .Include(s => s.Product)
                .Where(s => s.ProductCategoryId == category.Id && s.Product.IsActive && !s.Product.IsDelete)
                .Select(s => s.Product)
                .OrderByDescending(s => s.CreateDate)
                .Distinct()
                .Take(count)
                .ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryByUrlName(string productCategoryUrlName)
        {
            return await _productCategoryRepository.GetQuery()
                .AsQueryable()
                .SingleOrDefaultAsync(s => s.UrlName == productCategoryUrlName);
        }


        #region product gallery

        public async Task<List<ProductGallery>> GetAllProductGalleries(long productId)
        {
            return await _productGalleryRepository.GetQuery().AsQueryable().Where(g => g.ProductId == productId).ToListAsync();
        }

        public async Task<Product> GetProductBySellerOwnerId(long productId, long userId)
        {
            return await _productRepository.GetQuery().Include(p => p.Seller)
                .SingleOrDefaultAsync(p => p.Id == productId && p.Seller.UserId == userId);
        }

        public async Task<List<ProductGallery>> GetAllProductGalleriesInSellerPanel(long productId, long sellerId)
        {
            return await _productGalleryRepository.GetQuery()
                .Include(s => s.Product)
                .Where(s => s.ProductId == productId && s.Product.SellerId == sellerId).ToListAsync();
        }

        public async Task<CreateOrEditProductGalleryResult> CreateProductGallery(CreateOrEditProductGalleryDTO gallery, long productId, long sellerId)
        {
            var product = await _productRepository.GetEntityById(productId);
            if(product == null) return CreateOrEditProductGalleryResult.ProductNotFound;
            if(product.SellerId != sellerId) return CreateOrEditProductGalleryResult.NotForUserProduct;
            if(gallery.Image == null || !gallery.Image.IsImage()) return CreateOrEditProductGalleryResult.ImageIsNull;

            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(gallery.Image.FileName);

            gallery.Image.AddImageToServer(imageName, PathExtension.ProductGalleryImageServer, 100, 100, PathExtension.ProductGalleryThumbnailImageServer);

            await _productGalleryRepository.AddEntity(new ProductGallery
            {
                ProductId = productId,
                DisplayPriority = gallery.DisplayPriority,
                ImageName = imageName,
            });
            await _productGalleryRepository.SaveChanges();

            return CreateOrEditProductGalleryResult.Success;
        }

        public async Task<CreateOrEditProductGalleryDTO> GetProductGalleryForEdit(long galleryId, long sellerId)
        {
            var gallery = await _productGalleryRepository.GetQuery()
                .Include(g => g.Product)
                .SingleOrDefaultAsync(g => g.Id == galleryId && g.Product.SellerId == sellerId);

            if (gallery == null) return null;

            return new CreateOrEditProductGalleryDTO
            {
                DisplayPriority = gallery.DisplayPriority,
                ImageName = gallery.ImageName
            };
        }

        public async Task<CreateOrEditProductGalleryResult> EditProductGallery(long galleryId, long sellerId,
            CreateOrEditProductGalleryDTO gallery)
        {
            var mainGallery = await _productGalleryRepository.GetQuery()
                .Include(g => g.Product)
                .SingleOrDefaultAsync(g => g.Id == galleryId);

            if (mainGallery == null) return CreateOrEditProductGalleryResult.ProductNotFound;

            if (mainGallery.Product.SellerId != sellerId) return CreateOrEditProductGalleryResult.NotForUserProduct;

            if(gallery.Image != null && gallery.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(gallery.Image.FileName);

                var result = gallery.Image.AddImageToServer(imageName, PathExtension.ProductGalleryImageServer, 100, 100, PathExtension.ProductGalleryThumbnailImageServer, mainGallery.ImageName);
                mainGallery.ImageName = imageName;
            }
            mainGallery.DisplayPriority = gallery.DisplayPriority;

            _productGalleryRepository.EditEntity(mainGallery);
            await _productGalleryRepository.SaveChanges();

            return CreateOrEditProductGalleryResult.Success;
        }

        #endregion

        #region product feature

        public async Task CreateProductFeatures(long productId, List<CreateProductFeatureDTO> features)
        {
            var newFeatures = new List<ProductFeature>();
            if (features != null && features.Any())
            {
                foreach (var feature in features)
                {
                    newFeatures.Add(new ProductFeature()
                    {
                        FeatureTitle = feature.Feature,
                        FeatureValue = feature.FeatureValue,
                        ProductId = productId,
                    });
                }
                await _productFeatureRepository.AddRangeEntities(newFeatures);
                await _productFeatureRepository.SaveChanges();
            }
        }

        public async Task RemoveAllProductFeatures(long productId)
        {
            var productFeatures = await _productFeatureRepository.GetQuery().AsQueryable().Where(f => f.ProductId == productId).ToListAsync();

            _productFeatureRepository.DeletePermanentEntities(productFeatures);
            await _productFeatureRepository.SaveChanges();
        }

        #endregion

        #region Dispose
        public async ValueTask DisposeAsync()
        {
            await _productCategoryRepository.DisposeAsync();
            await _productRepository.DisposeAsync();
            await _productSelectedCategoryRepository.DisposeAsync();
            await _productFeatureRepository.DisposeAsync();
            await _productSelectedCategoryRepository.DisposeAsync();
            await _productColorRepository.DisposeAsync();
        }
        #endregion
    }
}
