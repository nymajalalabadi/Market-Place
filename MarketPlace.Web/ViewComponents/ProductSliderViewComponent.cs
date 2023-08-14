using MarketPlace.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.ViewComponents
{
    public class ProductSliderViewComponent : ViewComponent
    {
        #region constructor

        private readonly IProductService _productService;

        public ProductSliderViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(string categoryName)
        {
            var category = await _productService.GetProductCategoryByUrlName(categoryName);

            var products = await _productService.GetCategorysProductsByCategoryName(categoryName);

            ViewBag.title = category?.Title;

            return View("ProductSlider", products);
        }
    }
}
