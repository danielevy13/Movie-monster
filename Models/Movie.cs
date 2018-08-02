using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor;
using MovieMonste.ModelComponent;

namespace MovieMonste.Models
{
    public class Movie
    {
        //public Movie(Sale[] sale)
        //{
        //    this.Sales = new HashSet<Sale>(sale);
        //}
        public string MovieID { get; set; }
        public string Title { get; set; }
        public string Genere { get; set; }
        public int UnitsInStock { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Actors { get; set; }
        public int MinAge { get; set; }
        public string Language { get; set; }
        public Price UnitPrice { get; set; }
        //public virtual ICollection<Sale> Sales { get; set; }
    }
}
