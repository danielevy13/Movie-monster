using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieMonster.Models;

namespace MovieMonster.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // config 2 primery key for MovieSalemodelBuilder.Entity<MovieStockOrder>().HasKey(mso => new { mso.MovieID, mso.StockOrderID });
            modelBuilder.Entity<MovieSale>().HasKey(ms => new { ms.MovieID, ms.SaleID });
            // config 2 primery key for MovieStockOrder
            modelBuilder.Entity<MovieStockOrder>().HasKey(mso => new { mso.MovieID, mso.StockOrderID });
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<MovieSale> MovieSale { get; set; }

        public DbSet<Sale> Sale { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<StockOrder> StockOrder { get; set; }

        public DbSet<MovieStockOrder> MovieStockOrder { get; set; }
    }
}
