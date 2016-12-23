using System;

namespace Mvc5Application1.Models
{
    public partial class umbProducts : object
    {
        public int product_id { get; set; } // product_id (Primary key)
        public int category_id { get; set; } // category_id
        public short published { get; set; } // published
        public double rating_cache { get; set; } // rating_cache
        public int rating_count { get; set; } // rating_count
        public string name { get; set; } // name
        public double pricing { get; set; } // pricing
        public string short_description { get; set; } // short_description
        public string long_description { get; set; } // long_description
        public string icon { get; set; } // icon
        public DateTime created_at { get; set; } // created_at
        public DateTime updated_at { get; set; } // updated_at

        public umbProducts()
        {
            published = 0;
        }
    }
}