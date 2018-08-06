using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMonste.Models
{
    public class MovieStockOrder
    {
        [Key]
        public string StockOrderID { get; set; }
        [Key]
        public string MovieID { get; set; }
        public int Quantity { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual StockOrder StockOrder { get; set; }
    }
}
