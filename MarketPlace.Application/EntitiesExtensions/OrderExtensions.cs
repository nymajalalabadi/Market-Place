using MarketPlace.DataLayer.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.EntitiesExtensions
{
    public static class OrderExtensions
    {
        public static int GetOrderDetailWithDiscountPriceAmount(this UserOpenOrderDetailItemDTO detail)
        {
            if (detail.DiscountPercentage != null)
            {
                return ((detail.ProductPrice + detail.ProductColorPrice) * detail.DiscountPercentage.Value / 100 * detail.Count);
            }

            return 0;
        }

        public static string GetOrderDetailWithDiscountPrice(this UserOpenOrderDetailItemDTO detail)
        {
            if (detail.DiscountPercentage != null )
            {
                return detail.GetOrderDetailWithDiscountPriceAmount().ToString("#,0 تومان");
            }

            return "------";
        }
    }
}
