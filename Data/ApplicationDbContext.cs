﻿using Microsoft.EntityFrameworkCore;
using Stock.Models;

namespace Stock.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Phone)
                .IsUnique();

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Menu)
                .WithMany()
                .HasForeignKey(oi => oi.MenuId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
            modelBuilder.Entity<Bill>()
             .HasOne(b => b.Customer)
             .WithMany(c => c.Bills)
             .HasForeignKey(b => b.CustomerId)
             .OnDelete(DeleteBehavior.Cascade);

            // Employee 1 ⇢ * Bills
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Employee)
                .WithMany(e => e.Bills)
                .HasForeignKey(b => b.EmpId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
