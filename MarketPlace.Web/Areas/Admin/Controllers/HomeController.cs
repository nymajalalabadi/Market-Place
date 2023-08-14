using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region constractor
        //private readonly IUserService _userService;

        public HomeController()
        {
            
        }

        #endregion

        #region index

        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
