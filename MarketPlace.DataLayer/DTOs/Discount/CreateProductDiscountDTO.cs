﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.DTOs.Discount
{
    public class CreateProductDiscountDTO
    {
        public long ProductId { get; set; }

        [Range(0, 100)]
        public int Percentage { get; set; }

        public string ExpireDate { get; set; }

        public int DiscountNumber { get; set; }
    }

    public enum CreateDiscountResult
    {
        Success,
        ProductIsNotForSeller,
        ProductNotFound,
        Error
    }
}
