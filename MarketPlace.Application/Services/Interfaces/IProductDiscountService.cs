using MarketPlace.DataLayer.DTOs.Discount;
using MarketPlace.DataLayer.DTOs.ProductDiscounts;
using MarketPlace.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IProductDiscountService : IAsyncDisposable
    {
        #region crud product discount

        Task<FilterProductDiscountDTO> filterProductDiscount(FilterProductDiscountDTO filter);

        Task<CreateDiscountResult> CreateProductDiscount(CreateProductDiscountDTO discount , long sellerId);

        #endregion
    }
}
