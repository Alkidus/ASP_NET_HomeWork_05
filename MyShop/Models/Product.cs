using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
