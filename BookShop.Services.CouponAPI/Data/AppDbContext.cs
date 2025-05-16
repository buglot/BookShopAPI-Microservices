using BookShop.Services.CouponAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "DEQQ",
                DiscountAmount = 20,
                MinAmount = 20,
            }
            );
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "AWEQQ",
                DiscountAmount = 30,
                MinAmount = 20,
            }
            );
        }
    }
}