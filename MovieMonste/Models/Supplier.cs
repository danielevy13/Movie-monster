using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieMonste.ModelComponent;

namespace MovieMonste.Models
{
    public class Supplier
    {
        public string SupplierID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public Address SupplierAddress  { get; set; }
    }

}
