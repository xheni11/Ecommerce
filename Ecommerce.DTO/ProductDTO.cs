using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsPublic { get; set; }
    }
}
