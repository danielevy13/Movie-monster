using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMonster.Models
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int ApartmentNumber { get; set; }
        public int ZipCode { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
