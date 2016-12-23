namespace Mvc5Application1.Models
{
    public class umbCategories
    {
        public int category_id { get; set; } // category_id (Primary key)
        public int? category_parent_id { get; set; } // category_parent_id
        public string category_name { get; set; } // category_name
        public string description { get; set; } // description
        public byte[] picture { get; set; } // picture

        public umbCategories()
        {
        }
    }
}