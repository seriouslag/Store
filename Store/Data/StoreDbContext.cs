using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Store.Models;

namespace Store.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext() : base("name=StoreDbConnectionString")
        {
            Database.SetInitializer(new StoreDbInitializer());
            //Database.SetInitializer<StoreDbContext>(new DropCreateDatabaseAlways<StoreDbContext>());
            //Database.SetInitializer<StoreDbContext>(new DropCreateDatabaseIfModelChanges<StoreDbContext>());
            //Database.SetInitializer<StoreDbContext>(new DropCreateDatabaseAlways<StoreDbContext>());
            //Database.SetInitializer<StoreDbContext>(new StoreDbContext());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DatePrice> DatePrices { get; set; }
        public DbSet<DateValue> DateValues { get; set; }
    }
}