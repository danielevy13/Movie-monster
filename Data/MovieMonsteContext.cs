using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieMonste.Models
{
    public class MovieMonsteContext : DbContext
    {
        public MovieMonsteContext (DbContextOptions<MovieMonsteContext> options)
            : base(options)
        {
          
        }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Sale> Sale { get; set; }

        public DbSet<StockOrder> StockOrder { get; set; }

        public DbSet<Supplier> Supplier { get; set; }
    }
}
