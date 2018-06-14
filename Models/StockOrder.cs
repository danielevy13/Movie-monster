using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMansterWebApp.Models
{
    public class StockOrder
    {
        //public StockOrder()
        //{
        //    this.Movies = new HashSet<Movie>();
        //}
        public int StockOrderID { get; set; }
        public int SupplierID { get; set; }
        public int TotalPrice { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
