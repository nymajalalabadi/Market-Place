using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using MarketPlace.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class UploaderController : SiteBaseController
    {
        [HttpPost]
        public IActionResult UploadImage(IFormFile upload , string CKEditorFuncName , string CKEditor , string LangCode )
        {
            if (upload.Length <= 0) return null;

            if (!upload.IsImage())
            {
                var notImageMessage = "لطفا یا عکس انتخاب کنید ";
                var notImage = JsonConvert.DeserializeObject("{'uploaded':0, 'error': {'message': \" " + notImageMessage + " \"}}");
                return Json(notImage);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            upload.AddImageToServer(fileName , PathExtension.UploadImageServer , null , null);

            return Json(new 
            {
                uploaded = true,
                url = $"{PathExtension.UploadImage}{fileName}"
            });
        }
    }
}
