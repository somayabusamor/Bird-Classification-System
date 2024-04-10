using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROJECT_2024.Models
{
    public class CartItem
    {
        public int ProductId { get; set; } // Foreign key referencing Product table
        public Product Product { get; set; } // Reference to the actual product details
        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}