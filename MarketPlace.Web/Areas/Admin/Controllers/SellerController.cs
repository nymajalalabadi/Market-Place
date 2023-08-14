using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Seller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Web.Http;
using MarketPlace.DataLayer.DTOs.Common;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    public class SellerController : AdminBaseController
    {
        #region constractor
        private readonly ISellerService _sellerrService;

        public SellerController(ISellerService sellerrService)
        {
            _sellerrService = sellerrService;
        }

        #endregion

        #region seller request

        public async Task<IActionResult> SellerRequests(FilterSellerDTO filter)
        {
            filter.TakeEntity = 1;
            return View(await _sellerrService.FilterSellers(filter));
        }

        #endregion

        #region accept seller request

        public async Task<IActionResult> AcceptSellerRequest(long requestId)
        {
            var result = await _sellerrService.AcceptSellerRequest(requestId);

            if (result)
            {
                return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success ,"در خواست شما با موفقیت تایید شد" , null);
            }
            return JsonResponseStatus.SendStatus(JsonResponseStatusType.Danger, "اطلاعاتی با این مشخصه یافت نشد", null);
        }

        #endregion

        #region reject seller request

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectSellerRequest(RejectItemDTO reject)
        {
            if (ModelState.IsValid)
            {
                var result = await _sellerrService.RejectSellerRequest(reject);

                if (result)
                {
                    return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "در خواست شما با موفقیت رد شد", result);
                }
            }
            return JsonResponseStatus.SendStatus(JsonResponseStatusType.Danger, "اطلاعاتی با این مشخصه یافت نشد", null);
        }

        #endregion
    }
}
