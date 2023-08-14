using MarketPlace.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.Seller.Controllers
{
    [Authorize]
    [Area("Seller")]
    [Route("seller")]
    [CheckSellerState]
    public class SellerBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMassage";
        protected string SuccessMessage = "SuccessMassage";
        protected string InfoMessage = "InfoMassage";
        protected string WarningMessage = "WarningMassage";
    }
}
