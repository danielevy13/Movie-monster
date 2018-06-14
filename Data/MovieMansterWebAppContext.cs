using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieMansterWebApp.Models;
using Microsoft.AspNetCore.Razor;

namespace MovieMansterWebApp.Models
{
    public class MovieMansterWebAppContext : DbContext
    {
        public MovieMansterWebAppContext(DbContextOptions<MovieMansterWebAppContext> options)
            : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Supplier>().OwnsOne(s => s.SupplierAddress);
        //}
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<StockOrder> StockOrder { get; set; }
    }
}
