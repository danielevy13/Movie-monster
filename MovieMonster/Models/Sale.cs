﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor;

namespace MovieMonster.Models
{
    public class Sale
    {
        public string SaleID { get; set; }
        public string CustomerID { get; set; }
        public bool Purchased { get; set; }
        public int TotalPrice { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<MovieSale> Movies { get; set; }
    }
}
