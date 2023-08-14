using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Entities.ProductOrder
{
    public class Order : BaseEntity
    {
        #region properties

        public long UserId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public bool IsPaid { get; set; }

        [Display(Name = "کد پیگیری")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string TracingCode { get; set; }

        [Display(Name = "کد پیگیری")]
        public string Description { get; set; }

        #endregion

        #region relations

        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}
