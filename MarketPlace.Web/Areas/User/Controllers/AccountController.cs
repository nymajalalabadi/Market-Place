using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.Web.PresentationsExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class AccountController : UserBaseController
    {
        #region constractor
        private readonly IUserService _userService;    

        public AccountController(IUserService userService)
        {
            _userService = userService;          
        }

        #endregion

        #region user ChangePassword

        [HttpGet("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }


        [HttpPost("change-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO passwordDto)
        {
            if(ModelState.IsValid)
            {
                var res = await _userService.ChangeUserPassword(passwordDto, User.GetUserId());
                if (res)
                {
                    TempData[SuccessMessage] = "کلمه ی عبور شما تغییر یافت";
                    TempData[InfoMessage] = "لطفا جهت تکمیل فرایند تغییر کلمه ی عبور ، مجددا وارد سایت شوید";
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("Login", "Account", new { area = "" });
                }
                else
                {
                    TempData[ErrorMessage] = "لطفا از کلمه ی عبور جدیدی استفاده کنید";
                    ModelState.AddModelError("NewPassword", "لطفا از کلمه ی عبور جدیدی استفاده کنید");
                }
            }
            return View(passwordDto);
        }
        #endregion


        [HttpGet("edi-profile")]
        public async Task<IActionResult> EditProfile()
        {
            var userProfile = await _userService.GetProfileForEdit(User.GetUserId());
            if (userProfile == null) return NotFound();
            return View(userProfile);
        }


        [HttpPost("edi-profile") , ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditUserProfileDTO profile , IFormFile avatarImage)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditUserProfile(profile, User.GetUserId(), avatarImage);
                switch (result)
                {
                    case EditUserProfileResult.IsBlocked:
                        TempData[ErrorMessage] = "حساب کاربری شما بلاک شده است";
                        break;
                    case EditUserProfileResult.IsNotActive:
                        TempData[ErrorMessage] = "حساب کاربری شما فعال نشده است";
                        break;
                    case EditUserProfileResult.NotFound:
                        TempData[ErrorMessage] = "کاربری با مشصخات وارد شده یافت نشد";
                        break;
                    case EditUserProfileResult.Success:
                        TempData[SuccessMessage] = $"جناب {profile.FirstName} {profile.LastName}، پروفایل شما با موفقیت ویرایش شد";

                        return RedirectToAction("EditProfile");
                }
            }
                return View(profile);
        }

        #region edit profile



        #endregion
    }
}
