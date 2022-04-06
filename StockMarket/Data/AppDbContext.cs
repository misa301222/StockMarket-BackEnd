using Microsoft.AspNetCore.Identity;
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
    }
}
