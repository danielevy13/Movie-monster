﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMansterWebApp.ModelComponent

{
    [Microsoft.EntityFrameworkCore.Owned]
    public class Price
    { 
        public float Wholesale { get; set; }
        public float Retail { get; set; }
    }
}
