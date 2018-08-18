using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMonster.Models
{
    public class StockOrder
    {
        public string StockOrderID { get; set; }
        public string SupplierID { get; set; }
        public int TotalPrice { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<MovieStockOrder> Movies { get; set; }
    }
}
