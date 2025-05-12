namespace BookShop.Services.CouponAPI.Model
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public required string CouponCode { get; set; }
        public double DiscountAmount    { get; set; }
        public int MinAmount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
