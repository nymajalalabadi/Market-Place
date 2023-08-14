using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Controllers
{
    public class ProductController : SiteBaseController
    {
        #region constructor

        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;

        public ProductController(IProductService productService, ISellerService sellerService)
        {
            _productService = productService;
            _sellerService = sellerService;
        }

        #endregion

        #region filter products

        [HttpGet("products")]
        [HttpGet("products/{Category}")]
        public async Task<IActionResult> FilterProducts(FilterProductDTO filter)
        {
            filter.TakeEntity = 9;
            filter.FilterProductState = FilterProductState.Active;
            filter = await _productService.FilterProduct(filter);

            ViewBag.ProductCategories = await _productService.GetAllActiveProductCategories();

            if (filter.PageId > filter.GetLastPage() && filter.GetLastPage() != 0) return NotFound();
            
            return View(filter);
        }

        #endregion

        #region show product detail

        [HttpGet("products/{productId}/{title}")]
        public async Task<IActionResult> ProductDetail(long productId, string title)
        {
            var product = await _productService.GetProductDetailById(productId);

            if (product == null) return NotFound();

            return View(product);
        }

        #endregion
    }
}
