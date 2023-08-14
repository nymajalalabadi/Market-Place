using MarketPlace.Application.Services.Interfaces;
using MarketPlace.Web.PresentationsExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Http
{
    public class CheckSellerStateAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private ISellerService _sellerService;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _sellerService = (ISellerService)context.HttpContext.RequestServices.GetService(typeof(ISellerService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetUserId();

                if (!_sellerService.HasUserAnyActiveSellerPanel(userId).Result)
                {
                    context.Result = new RedirectResult("/user");
                }
            }
        }
    }
}
