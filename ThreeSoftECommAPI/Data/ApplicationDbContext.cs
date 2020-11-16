using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<SubCategory> subCategory { get; set; }
        public DbSet<UserAddresses> UserAddresses { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductAttributes> ProductAttributes { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductReviews> ProductReviews { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<UserFavourites> UserFavourites { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotificationCount> userNotificationCounts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity => {entity.HasIndex(e => e.ArabicName).IsUnique();});
            modelBuilder.Entity<Category>(entity => {entity.HasIndex(e => e.EnglishName).IsUnique();});

            modelBuilder.Entity<SubCategory>(entity => {entity.HasIndex(e => e.ArabicName).IsUnique();});
            modelBuilder.Entity<SubCategory>(entity => {entity.HasIndex(e => e.EnglishName).IsUnique();});

            modelBuilder.Entity<Advertising>(entity => {entity.HasIndex(e => e.ArabicDescription).IsUnique();});
            modelBuilder.Entity<Advertising>(entity => {entity.HasIndex(e => e.EnglishDescription).IsUnique();});

            modelBuilder.Entity<Brand>(entity => {entity.HasIndex(e => e.ArabicName).IsUnique();});
            modelBuilder.Entity<Brand>(entity => {entity.HasIndex(e => e.EnglishName).IsUnique();});

            modelBuilder.Entity<Coupon>(entity => {entity.HasIndex(e => e.Code).IsUnique();});

            modelBuilder.Entity<ProductAttributes>(entity => {entity.HasIndex(e => e.ArabicName).IsUnique();});
            modelBuilder.Entity<ProductAttributes>(entity => {entity.HasIndex(e => e.EnglishName).IsUnique();});

            //modelBuilder.Entity<ProductColors>(entity => {entity.HasIndex(e => e.ArabicName).IsUnique();});
            //modelBuilder.Entity<ProductColors>(entity => {entity.HasIndex(e => e.EnglishName).IsUnique();});
            

            
            modelBuilder.Entity<Category>().Property(x => x.ArabicName).IsRequired();
            modelBuilder.Entity<Category>().Property(x => x.EnglishName).IsRequired();

            modelBuilder.Entity<SubCategory>().Property(x => x.ArabicName).IsRequired();
            modelBuilder.Entity<SubCategory>().Property(x => x.EnglishName).IsRequired();

            modelBuilder.Entity<Brand>().Property(x => x.ArabicName).IsRequired();
            modelBuilder.Entity<Brand>().Property(x => x.EnglishName).IsRequired();

            modelBuilder.Entity<ProductAttributes>().Property(x => x.ArabicName).IsRequired();
            modelBuilder.Entity<ProductAttributes>().Property(x => x.EnglishName).IsRequired();

            //modelBuilder.Entity<ProductColors>().Property(x => x.ArabicName).IsRequired();
            //modelBuilder.Entity<ProductColors>().Property(x => x.EnglishName).IsRequired();


            modelBuilder.Entity<UserAddresses>().Property(x => x.Lat).HasColumnType("DECIMAL (10,8)");
            modelBuilder.Entity<UserAddresses>().Property(x => x.Lon).HasColumnType("DECIMAL (11,8)");
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("DECIMAL (25,3)");
            modelBuilder.Entity<Product>().Property(x => x.SalePrice).HasColumnType("DECIMAL (25,3)");
            modelBuilder.Entity<Coupon>().Property(x => x.Amount).HasColumnType("DECIMAL (25,3)");
            modelBuilder.Entity<Payment>().Property(x => x.Amount).HasColumnType("DECIMAL (25,3)");
            modelBuilder.Entity<Offers>().Property(x => x.offerPrice).HasColumnType("DECIMAL (25,3)");
            modelBuilder.Entity<OrderItems>().Property(x => x.Price).HasColumnType("DECIMAL (25,3)");
            modelBuilder.Entity<OrderItems>().Property(x => x.Total).HasColumnType("DECIMAL (25,3)");

            modelBuilder.Entity<Product>().Property(x => x.SalePrice).HasDefaultValue(0);

            modelBuilder.Entity<UserNotificationCount>().Property(x => x.NotificationCount).HasDefaultValue(0);

            

        }
    }
}
