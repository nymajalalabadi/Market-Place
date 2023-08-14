using MarketPlace.Application.Services.Interfaces;
using MarketPlace.Application.Utils;
using MarketPlace.DataLayer.DTOs.Discount;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.DTOs.ProductDiscounts;
using MarketPlace.DataLayer.Entities.Products;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class ProductDiscountService : IProductDiscountService
    {
        #region constructor

        private readonly IGenericRepository<ProductDiscount> _productDiscountRepository;
        private readonly IGenericRepository<ProductDiscountUse> _productDiscountUseRepository;
        private readonly IGenericRepository<Product> _ProductRepository;

        public ProductDiscountService(IGenericRepository<ProductDiscount> productDiscountRepository , IGenericRepository<ProductDiscountUse> productDiscountUseRepository, IGenericRepository<Product> ProductRepository)
        {
            _productDiscountRepository = productDiscountRepository;
            _productDiscountUseRepository = productDiscountUseRepository;
            _ProductRepository = ProductRepository;
        }

        #endregion

        #region crud product discount

        public async Task<FilterProductDiscountDTO> filterProductDiscount(FilterProductDiscountDTO filter)
        {
            var query = _productDiscountRepository.GetQuery()
                .Include(s => s.Product)
                .AsQueryable();

            #region filter

            if (filter.ProductId != null && filter.ProductId != 0)
                query = query.Where(s => s.ProductId == filter.ProductId.Value);

            if (filter.SellerId != null && filter.SellerId != 0)
                query = query.Where(s => s.Product.SellerId == filter.SellerId.Value);

            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetDiscounts(allEntities);
        }

        public async Task<CreateDiscountResult> CreateProductDiscount(CreateProductDiscountDTO discount, long sellerId)
        {
            var product = await _ProductRepository.GetEntityById(discount.ProductId);

            if (product == null) return CreateDiscountResult.ProductNotFound;

            if (product.SellerId != sellerId) return CreateDiscountResult.ProductIsNotForSeller;

            var newDiscount = new ProductDiscount()
            {
                ProductId = discount.ProductId,
                DiscountNumber = discount.DiscountNumber,
                ExpireDate = discount.ExpireDate.ToMiladiDateTime(),
                Percentage = discount.Percentage
            };

            await _productDiscountRepository.AddEntity(newDiscount);
            await _productDiscountRepository.SaveChanges();

            return CreateDiscountResult.Success;
        }

        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            await _productDiscountRepository.DisposeAsync();
            await _productDiscountUseRepository.DisposeAsync();
            await _ProductRepository.DisposeAsync();
        }

        #endregion
    }
}
