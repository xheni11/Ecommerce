
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.DAL.Entities
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedUser { get; set; }
    }
}
