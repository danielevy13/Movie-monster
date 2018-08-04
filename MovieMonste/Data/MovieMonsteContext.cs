using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieMonste.Models
{
    public class MovieMonsteContext : DbContext
    {
        public MovieMonsteContext (DbContextOptions<MovieMonsteContext> options)
            : base(options)
        {
        }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>(e =>
                e.HasOne(r => r.Customer).WithMany(c => c.Sales)
            );
        }
                protected override void OnModelCreating(ModelBuilder modelBuilder)
                {

                    modelBuilder.Entity<Sale>()
                                .HasMany<Movie>(m => m.Movie)
                                .WithMany(s => s.Sale)
                                .Map(cs =>
                                {
                                    cs.MapLeftKey("SaleId");
                                    cs.MapRightKey("MovieId");
                                    cs.ToTable("SaleMovies");
                                });

                }
                */
        public DbSet<Customer> Customer { get; set; }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Sale> Sale { get; set; }

        public DbSet<StockOrder> StockOrder { get; set; }

        public DbSet<Supplier> Supplier { get; set; }


    }
}
