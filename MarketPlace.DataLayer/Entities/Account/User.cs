using MarketPlace.DataLayer.Entities.Common;
using MarketPlace.DataLayer.Entities.Contacts;
using MarketPlace.DataLayer.Entities.Products;
using MarketPlace.DataLayer.Entities.Store;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.DataLayer.Entities.Account
{
    public class User : BaseEntity
    {
        #region properties

        [Display(Name = "Email")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        [EmailAddress(ErrorMessage = "Your Email Is Invalid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string EmailActiveCode { get; set; }

        [Display(Name = "Enabled Email / Disabled Email")]
        public bool IsEmailActive { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(20, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string MobileActiveCode { get; set; }

        [Display(Name = "Enabled Mobile / Disabled Mobile")]
        public bool IsMobileActive { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The {0} Is Required")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string LastName { get; set; }

        [Display(Name = "Avatar Picture")]
        [MaxLength(200, ErrorMessage = "{0} Cannot Be Longer Than {1} Characters")]
        public string Avatar { get; set; }

        [Display(Name = "Blocked / Unblocked")]
        public bool IsBlocked { get; set; }

        #endregion


        #region relations

        public ICollection<ContactUs> ContactUses { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<TicketMessage> TicketMessages { get; set; }
        public ICollection<Seller> Sellers { get; set; }
        public ICollection<ProductDiscountUse> ProductDiscountUses { get; set; }

        #endregion
    }
}
