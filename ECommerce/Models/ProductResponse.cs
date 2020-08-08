using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ProductResponse
    {
       public List<Product> listProduct { get; set; }
       public List<User> listUser { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Details { get; set; }

    }
}
