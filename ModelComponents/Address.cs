using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMansterWebApp.Models;

namespace MovieMansterWebApp.ModelComponent

{
    //[System.ComponentModel.DataAnnotations.Schema.ComplexType]
    [Microsoft.EntityFrameworkCore.Owned]
    public class Address
    {
        public string State { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int ApartmentNumber { get; set; }
        public int ZipCode { get; set; }
    }
}
