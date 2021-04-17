
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.DAL.Entities
{
    [Table("Discount")]
    public class Discount:BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("ProductId")]
        public Product Prdouct { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
        public double Percentage { get; set; }
    }
}
