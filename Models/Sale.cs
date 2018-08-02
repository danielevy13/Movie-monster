using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor;

namespace MovieMansterWebApp.Models
{
    public class Sale
    {
    //    public Sale()
     //   {
        //    this.CustomerID = Customer.CustomerID.;
        //   this.Movies = new HashSet<Movie>();
     //   }
        public string SaleID { get; set; }
     //   public string CustomerID { get; set; }
        public bool Purchased { get; set; }
        public int TotalPrice { get; set; }
      //  public virtual Customer Customer { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
