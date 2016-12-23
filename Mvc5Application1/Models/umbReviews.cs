using System;

namespace Mvc5Application1.Models
{
    // umbReviews
    public partial class umbReviews : object
    {
        public int review_id { get; set; } // review_id (Primary key)
        public int product_id { get; set; } // product_id
        public int user_id { get; set; } // user_id
        public int rating { get; set; } // rating
        public string comment { get; set; } // comment
        public short approved { get; set; } // approved
        public short spam { get; set; } // spam
        public DateTime created_at { get; set; } // created_at
        public DateTime updated_at { get; set; } // updated_at

        public umbReviews()
        {
            approved = 1;
            spam = 0;
        }
    }
}