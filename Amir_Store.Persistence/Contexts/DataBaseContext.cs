﻿using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Roles;
using Amir_Store.Domain.Entities.Carts;
using Amir_Store.Domain.Entities.Finance;
using Amir_Store.Domain.Entities.HomePage;
using Amir_Store.Domain.Entities.Orders;
using Amir_Store.Domain.Entities.Products;
using Amir_Store.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomePageImage> HomePageImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<RequestPay> RequestPays { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(p => p.User)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.RequestPay)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.NoAction);
            // Roles  افزودن مقادیر پیش فرض به جدول
            SeedData(modelBuilder);

            // اعمال ایندکس بر روی فیلد ایمیل
            // جلوگیری از ورود ایمیل تکراری
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            // read users that IsRemoved is False
            ApplyQueryFilters(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRoles.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRoles.Customer) });
        }
        private void ApplyQueryFilters(ModelBuilder modelBuilder)
        {
            //var type = typeof(DataBaseContext);
            //var DataBaseContextProperties = type.GetProperties();
            //foreach (var property in DataBaseContextProperties)
            //{
            //    var typeProperty = typeof(property);
            //    var ww = typeProperty.GetElementType();
            //    //modelBuilder.Entity<typeProperty>().HasQueryFilter(p => !p.IsRemoved);
            //}
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HomePageImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<RequestPay>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<RequestPay>().HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
