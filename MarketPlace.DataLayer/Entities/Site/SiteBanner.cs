using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Site
{
    public class SiteBanner : BaseEntity
    {
        #region properties

        [Display(Name = "Image Name")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string ImageName { get; set; }

        [Display(Name = "Url Adress")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Url { get; set; }

        [Display(Name = "Col Size")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(500, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string ColSize { get; set; }

        public BannerPlacement BannerPlacement { get; set; }

        #endregion
    }
}
