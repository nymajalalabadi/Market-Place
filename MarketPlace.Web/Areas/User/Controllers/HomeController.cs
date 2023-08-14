using MarketPlace.DataLayer.DTOs.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class HomeController : UserBaseController
    {
        #region constractor

        public HomeController()
        {

        }

        #endregion


        #region user DashBoard
        [HttpGet("")]
        public async Task<IActionResult> DashBoard()
        {
            return View();
        }
        #endregion
    }
}
