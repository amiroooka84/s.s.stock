using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Entity.UserApp;
using static Entity.UserApp.userapp;
using Entity.Address;
using Entity.ProductAndShop;
using Entity.AttributeProduct;
using Entity.Basket;
using Entity.Order;
using Entity.Interests;
using Entity.Comment;
using Entity.TrackingCode;

namespace dal
{
    public class db : IdentityDbContext<UserApp>
    {
        public db() : base() { }
        public db(DbContextOptions<db> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Shop_Stationery1;Integrated Security=True");
            //optionsBuilder.UseSqlServer("Server=.; Initial Catalog=stockss; User ID=stockss; Password=stockss.123; MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductOrder> ProductOrders { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Category> Categories  { get; set; } 
        public DbSet<Shop> Shops { get; set; } 
        public DbSet<ImagePath> ImagePath { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ProductAndShop> ProductAndShop { get; set; }
        public DbSet<AttributeProduct> AttributeProducts { get; set; }
        public DbSet<Basket> Baskets{ get; set; }
        public DbSet<Interests> Interests { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<TrackingCode> TrackingCodes { get; set; }
        
    }
}
