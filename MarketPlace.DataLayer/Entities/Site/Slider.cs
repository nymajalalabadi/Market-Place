using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Site
{
    public class Slider : BaseEntity
    {
        #region properties

        [Display(Name = "Main Header")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string MainHeader { get; set; }

        [Display(Name = "Second Header")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string SecondHeader { get; set; }

        [Display(Name = "Image Name")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string ImageName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Description { get; set; }

        [Display(Name = "Link")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Link { get; set; }

        [Display(Name = "Active / Deactive")]
        public bool IsActive { get; set; }

        #endregion
    }
}
