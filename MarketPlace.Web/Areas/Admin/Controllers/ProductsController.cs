using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Common;
using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.Web.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    public class ProductsController : AdminBaseController
    {
        #region constractor
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region filter product

        public async Task<IActionResult> Index(FilterProductDTO filter)
        {
            return View(await _productService.FilterProduct(filter));
        }

        #endregion

        #region accept product

        public async Task<IActionResult> AcceptSellerProduct(long Id)
        {
            var result = await _productService.AcceptSellerProduct(Id);

            if (result)
            {
                return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "محصول مورد نظر با موفقیت تایید شد", null);
            }
            return JsonResponseStatus.SendStatus(JsonResponseStatusType.Danger, "محصول مورد نظر یافت نشد", null);
        }

        #endregion

        #region reject product 
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectSellerProduct(RejectItemDTO reject)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.RejectSellerProduct(reject);
                if (result)
                {
                    return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "محصول مورد نظر با موفقیت رد شد", reject);
                }

                return JsonResponseStatus.SendStatus(JsonResponseStatusType.Danger,
                    "اطلاعات مورد نظر جهت عدم تایید را به درستی وارد نمایید", null);
            }
            return JsonResponseStatus.SendStatus(JsonResponseStatusType.Danger, "محصول مورد نظر یافت نشد", null);
        }

        #endregion
    }
}
