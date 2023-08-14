﻿using MarketPlace.DataLayer.DTOs.Common;
using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region product

        Task<FilterProductDTO> FilterProduct(FilterProductDTO filter);

        Task<CreateProductResult> CreateProduct(CreateProductDTO product, long sellerId , IFormFile productImage);

        Task<bool> AcceptSellerProduct(long productId);

        Task<bool> RejectSellerProduct(RejectItemDTO reject);

        Task<EditProductDTO> GetProductForEdit(long productId);

        Task<EditProductResult> EditSellerProduct(EditProductDTO product, long userId, IFormFile productImage);

        Task RemoveAllProductSelectedCategories(long productId);

        Task RemoveAllProductSelectedColors(long productId);

        Task AddProductSelectedColors(long productId , List<CreateProductColorDTO> colors);

        Task AddProductSelectedCategories(long productId , List<long> SelectedCategories);

        Task<ProductDetailDTO> GetProductDetailById(long productId);

        Task<List<Product>> FilterProductsForSellerByProductName(string ProductName, long sellerId);

        Task<List<ProductDiscount>> GetAllOffProducts(int take);

        #endregion

        #region product categories

        Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId);

        Task<List<ProductCategory>> GetAllActiveProductCategories();

        Task<List<Product>> GetCategorysProductsByCategoryName(string categoryName, int count = 12);

        Task<ProductCategory> GetProductCategoryByUrlName(string productCategoryUrlName);

        #endregion


        #region product gallery

        Task<List<ProductGallery>> GetAllProductGalleries(long productId);

        Task<Product> GetProductBySellerOwnerId(long productId, long userId);

        Task<List<ProductGallery>> GetAllProductGalleriesInSellerPanel(long productId, long sellerId);

        Task<CreateOrEditProductGalleryResult> CreateProductGallery(CreateOrEditProductGalleryDTO gallery, long productId, long sellerId);

        Task<CreateOrEditProductGalleryDTO> GetProductGalleryForEdit(long galleryId, long sellerId);

        Task<CreateOrEditProductGalleryResult> EditProductGallery(long galleryId, long sellerId, CreateOrEditProductGalleryDTO gallery);

        #endregion

        #region product feature

        Task CreateProductFeatures(long productId, List<CreateProductFeatureDTO> features);
        Task RemoveAllProductFeatures(long productId);

        #endregion
    }
}
