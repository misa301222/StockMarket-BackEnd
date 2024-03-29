﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockMarket.Data.Entities;
using StockMarket.Data.Entity;

namespace StockMarket.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Stock>().HasKey(c => new { c.StockName });
            modelBuilder.Entity<UserPortfolio>().HasKey(c => new { c.Email, c.StockName });
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<StockBought> StockBought { get; set; }
        public DbSet<UserPortfolio> UserPortfolios { get; set; }
        public DbSet<UserProfit> UserProfit { get; set; }
        public DbSet<StockHistory> StockHistories { get; set; }
        public DbSet<StockSold> StockSold { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserProfitHistory> UserProfitHistories { get; set; }
        public DbSet<TradeStockHistory> TradeStockHistories { get; set; }
    }
}
