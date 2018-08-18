using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor;


namespace MovieMonster.Models
{
    public class Movie
    {

        public string MovieID { get; set; }
        public string Title { get; set; }
        public string Genere { get; set; }
        public int UnitsInStock { get; set; }
        public int YearRelease { get; set; }
        public string Actors { get; set; }
        public string Rated { get; set; }
        public string Language { get; set; }
        public float Wholesale { get; set; }
        public float Retail { get; set; }
       
    }
}
