using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Utils
{
    public static class PathExtension
    {
        #region domain address

        public static string DomainAddress = "https://localhost:44387";

        #endregion

        #region default images

        public static string DefaultAvatar = "/img/defaults/avatar.jpg";

        #endregion

        #region uploader

        public static string UploadImage = "/img/upload/";
        public static string UploadImageServer =Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/img/upload/");

        #endregion

        #region product

        public static string ProductImage = "/content/images/product/origin/";

        public static string ProductImageServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/product/origin/");


        public static string ProductThumbnailImage = "/content/images/product/thumb/";

        public static string ProductThumbnailImageImageServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/product/thumb/");

        #endregion

        #region product gallery

        public static string ProductGalleryImage = "/content/images/product-gallery/origin/";

        public static string ProductGalleryImageServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/product-gallery/origin/");


        public static string ProductGalleryThumbnailImage = "/content/images/product-gallery/thumb/";

        public static string ProductGalleryThumbnailImageServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/product-gallery/thumb/");
        #endregion

        #region user avatar

        public static string UserAvatarOrigin = "/Content/Images/UserAvatar/origin/";
        public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/Content/Images/UserAvatar/origin/");

        public static string UserAvatarThumb = "/Content/Images/UserAvatar/Thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/Content/Images/UserAvatar/Thumb/");

        #endregion


        #region slider

        public static string SliderOrigin = "/img/slider/";

        #endregion

        #region banner

        public static string bannerOrigin = "/img/bg/";

        #endregion
    }
}
