using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using MovieMonste.Models;

namespace MovieMonste.Models
{
    public class MovieMonsteContext : DbContext
    {
        public MovieMonsteContext (DbContextOptions<MovieMonsteContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // config 2 primery key for MovieSale
            modelBuilder.Entity<MovieSale>().HasKey(ms => new { ms.MovieID, ms.SaleID });
            // config 2 primery key for MovieStockOrder
            modelBuilder.Entity<MovieStockOrder>().HasKey(mso => new { mso.MovieID, mso.StockOrderID});
        }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Sale> Sale { get; set; }

        public DbSet<StockOrder> StockOrder { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<MovieSale> MovieSale { get; set; }

        public DbSet<MovieMonste.Models.MovieStockOrder> MovieStockOrder { get; set; }

    }
}
