using GoogleReCaptcha.V3.Interface;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Common;
using MarketPlace.DataLayer.DTOs.Contacts;
using MarketPlace.DataLayer.Entities.Site;
using MarketPlace.Web.Models;
using MarketPlace.Web.PresentationsExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        #region constructor

        private readonly IContactService _contactService;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly ISiteService _siteService;
        private readonly IProductService _productService;

        public HomeController(IContactService contactService, ICaptchaValidator captchaValidator , ISiteService siteService, IProductService productService)
        {
            _contactService = contactService;
            _captchaValidator = captchaValidator;
            _siteService = siteService;
            _productService = productService;
        }

        #endregion


        #region index

        public async Task<IActionResult> Index()
        {
            ViewBag.banners = await _siteService.GetSiteBannersByPlacement(new List<BannerPlacement>
            {
                BannerPlacement.Home_1,
                BannerPlacement.Home_2,
                BannerPlacement.Home_3
            });

            ViewData["Offproduct"] = await _productService.GetAllOffProducts(10);

            return View();
        }

        #endregion


        #region contact us
        [HttpGet("contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost("contact-us") , ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(CreateContactUsDTO contact)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(contact.Captcha))
            {
                TempData[ErrorMassage] = "کد کپچای شما تایید نشد";
                return View(contact);
            }

            if (ModelState.IsValid)
            {
                await _contactService.CreateContactUs(contact , HttpContext.GetUserIp() , User.GetUserId());
                TempData[SuccessMassage] = "پیام شما با موفقیت ارسال شد";
                return RedirectToAction("ContactUs");
            }
            return View(contact);
        }

        #endregion


        #region about us

        [HttpGet("about-us")]
        public async Task<IActionResult> AboutUs()
        {
            var siteSetting = _siteService.GetDefaultSiteSetting();
            return View(siteSetting);
        }

        #endregion

    }
}
