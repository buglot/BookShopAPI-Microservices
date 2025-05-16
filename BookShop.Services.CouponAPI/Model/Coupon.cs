﻿using System.ComponentModel.DataAnnotations;

namespace BookShop.Services.CouponAPI.Model
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public required string CouponCode { get; set; }
        [Required]
        public double DiscountAmount    { get; set; }
        public int MinAmount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
