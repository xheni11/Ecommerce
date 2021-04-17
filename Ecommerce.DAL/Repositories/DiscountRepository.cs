using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositories
{
    public class DiscountRepository : BaseRepository<Discount>
    {
        public DiscountRepository(EcommerceContext context) : base(context)
        {
        }

        public  List<Discount> GetDiscountsByProduct(Product product)
        {
            return  _dbSet.Where(d=>d.Prdouct.Equals(product)).ToList();
        }
    }
}
