﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMansterWebApp.ModelComponent;

namespace MovieMansterWebApp.Models
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string MailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public char Gender { get; set; }
        public Address CustomerAddress { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }

    }
}
