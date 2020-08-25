using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Item
    {
        public int item_description_id { get; set; }
        public string item_description { get; set; }
        public string item_name { get; set; }
        public string item_price { get; set; }
        public string available_as { get; set; }
        public string category { get; set; }
        public int available_as_id { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }

    }
}
