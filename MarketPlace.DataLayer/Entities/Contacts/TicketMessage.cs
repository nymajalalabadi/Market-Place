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
    public class TicketMessage : BaseEntity
    {
        #region properties

        public long TicketId { get; set; }

        public long SenderId { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }

        #endregion

        #region relations

        public Ticket Ticket { get; set; }

        public User Sender { get; set; }

        #endregion
    }
}
