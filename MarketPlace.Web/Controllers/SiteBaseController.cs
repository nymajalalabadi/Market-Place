using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Controllers
{
    public class SiteBaseController : Controller
    {
        protected string ErrorMassage = "ErrorMassage";
        protected string SuccessMassage = "SuccessMassage";
        protected string InfoMassage = "InfoMassage";
        protected string WarningMassage = "WarningMassage";
    }
}
