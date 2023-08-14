using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.Contacts
{
    public class ContactUs : BaseEntity
    {
        #region properties

        public long? UserId { get; set; }

        [Display(Name = "User IP")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(50, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string UserIp { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string FullName { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Subject { get; set; }

        [Display(Name = "Text Message")]
        [Required(ErrorMessage = "The {0} Is Required")]
        public string Text { get; set; }


        #endregion


        #region relations

        public User User { get; set; }

        #endregion
    }
}
