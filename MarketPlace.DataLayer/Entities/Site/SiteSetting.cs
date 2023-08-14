using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Site
{
    public class SiteSetting : BaseEntity
    {
        #region properties

        [Display(Name = "Phone Number")]
        public string Mobile { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Footer Text")]
        public string FooterText { get; set; }

        [Display(Name = "Copy Right Text")]
        public string CopyRight { get; set; }

        [Display(Name = "Map Script")]
        public string MapScript { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "AboutUs")]
        public string AboutUs { get; set; }

        [Display(Name = "Is Default / IsNot Default")]
        public bool IsDefault { get; set; }

        #endregion
    }
}
