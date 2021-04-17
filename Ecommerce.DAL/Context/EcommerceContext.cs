using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Entities
{

        public class EcommerceContext : DbContext
        {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EcommerceContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {    
            _httpContextAccessor = httpContextAccessor;
        }

        public EcommerceContext() : base()
        {
        }


        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }

        }
    }

