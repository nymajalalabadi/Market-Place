using MarketPlace.Application.Extensions;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.Application.Utils;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor

        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHelper _passwordHelper;

        private readonly ISmsService _smsService;

        public UserService(IGenericRepository<User> userRepository , IPasswordHelper passwordHelper , ISmsService smsService)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _smsService = smsService;
        }

        #endregion

        #region account

        public async Task<RegisterUserResult> registerUser(RegsiterUserDTO regsiter)
        {
                if (!await IsUserExistsByMobileNumber(regsiter.Mobile))
                {
                    var user = new User()
                    {
                        FirstName = regsiter.FirstName,
                        LastName = regsiter.LastName,
                        Password = _passwordHelper.EncodePasswordMd5(regsiter.Password),
                        Mobile = regsiter.Mobile,
                        MobileActiveCode = new Random().Next(10000, 999999).ToString(),
                        EmailActiveCode = Guid.NewGuid().ToString("N")
                    };
                    await _userRepository.AddEntity(user);
                    await _userRepository.SaveChanges();

                    //todo active mobile 
                    await _smsService.SendVerificationSms(user.Mobile, user.MobileActiveCode);

                    return RegisterUserResult.Success;
                }
            return RegisterUserResult.MobileExists;
        }

        public async Task<bool> IsUserExistsByMobileNumber(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().AnyAsync(u => u.Mobile == mobile);
        }

        public async Task<LoginUserResult> GetUserForLogin(LoginUserDTO login)
        {
            var user = await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(u => u.Mobile == login.Mobile);
            if (user == null) return LoginUserResult.NotFound;
            if (!user.IsMobileActive) return LoginUserResult.NotActivated;
            if (user.Password != _passwordHelper.EncodePasswordMd5(login.Password)) return LoginUserResult.NotFound;
            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(u => u.Mobile == mobile);
        }

        public async Task<ForgotPasswordResult> RecoverUserPassword(ForgotPasswordDTO forgot)
        {
            var user = await _userRepository.GetQuery().SingleOrDefaultAsync(u => u.Mobile == forgot.Mobile);

            if (user == null) return ForgotPasswordResult.NotFound;

            var newpassword = new Random().Next(1000000, 999999999).ToString();
            user.Password = _passwordHelper.EncodePasswordMd5(newpassword);
            _userRepository.EditEntity(user);

            //send to mobile 
            await _smsService.SendUserPasswordSms(forgot.Mobile, newpassword);

            await _userRepository.SaveChanges();

            return ForgotPasswordResult.Success;
        }

        public async Task<bool> ActivateMobile(ActivateMobileDTO activate)
        {
            var user = await _userRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(s => s.Mobile == activate.Mobile);

            if(user != null)
            {
                if(user.MobileActiveCode==activate.MobileActiveCode)
                {
                    user.IsMobileActive = true;
                    user.MobileActiveCode = new Random().Next(10000, 999999).ToString();
                    await _userRepository.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ChangeUserPassword(ChangePasswordDTO changePass, long currentUserId)
        {
            var user = await _userRepository.GetEntityById(currentUserId);
            if(user != null)
            {
                var newPassword = _passwordHelper.EncodePasswordMd5(changePass.NewPassword);
                if (newPassword != user.Password)
                {
                    user.Password = newPassword;
                     _userRepository.EditEntity(user);
                     await _userRepository.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public async Task<EditUserProfileDTO> GetProfileForEdit(long userId)
        {
            var user = await _userRepository.GetEntityById(userId);
            if (user == null) return null;

            return new EditUserProfileDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar
            };
        }


        public async Task<EditUserProfileResult> EditUserProfile(EditUserProfileDTO profile, long userId, IFormFile avatarImage)
        {
            var user = await _userRepository.GetEntityById(userId);

            if (user == null) return EditUserProfileResult.NotFound;
            if (user.IsBlocked) return EditUserProfileResult.IsBlocked;
            if (!user.IsMobileActive) return EditUserProfileResult.IsNotActive;

            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName ;

            if (avatarImage != null && avatarImage.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(avatarImage.FileName);

                avatarImage.AddImageToServer
                    (imageName, PathExtension.UserAvatarOriginServer,100,100,PathExtension.UserAvatarThumbServer,user.Avatar);

                user.Avatar = imageName;
            }

            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();

            return EditUserProfileResult.Success;
        }

        #endregion

        #region Dispose
        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }        
        #endregion

    }
}
