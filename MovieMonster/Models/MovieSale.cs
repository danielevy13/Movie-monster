using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMonster.Models
{
    public class MovieSale
    {
        [Key]
        public string SaleID  { get; set; }
        [Key]
        public string MovieID { get; set; }
        public int Quantity{ get; set; }
        public virtual Movie Movie{ get; set; }
        public virtual Sale Sale { get; set; }
    }
}
