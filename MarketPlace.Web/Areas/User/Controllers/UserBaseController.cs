using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    [Route("user")]
    public class UserBaseController : Controller 
    {
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
        protected string InfoMessage = "InfoMessage";
        protected string ErrorMessage = "ErrorMessage";
    }
}
