using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
        public Company()
        {
            Products = new List<Product>();
        }
    }
}
