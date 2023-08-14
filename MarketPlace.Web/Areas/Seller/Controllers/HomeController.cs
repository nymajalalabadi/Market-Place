using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.Seller.Controllers
{
    public class HomeController : SellerBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
