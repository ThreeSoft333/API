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
        public DbSet<Category> category { get; set; }
        public DbSet<SubCategory> subCategory { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
    }
}
