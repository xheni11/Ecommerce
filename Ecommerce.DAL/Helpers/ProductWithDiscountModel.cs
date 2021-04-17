using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Helpers
{
    public class ProductWithDiscountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public bool IsPublic { get; set; }
    }
}
