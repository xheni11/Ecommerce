using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
