using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecommerce.DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        private const int _pageSize = 10;
        public OrderRepository(EcommerceContext context) : base(context)
        {
        }
        public List<Order> GetAll(int pageNumber, string? name, string? userName, DateTime? dateFrom, DateTime? dateTo)
        {
            return _dbSet.Where(p => p.IsDeleted == false &&
                     (p.User.UserName.Contains(userName) || string.IsNullOrEmpty(userName)) &&
                     (p.Prdouct.Name.Contains(name) || string.IsNullOrEmpty(name)) &&
                     (p.CreatedOn >= (dateFrom.HasValue ? dateFrom : new DateTime(1900, 1, 1, 8, 30, 52)) &&
                         p.CreatedOn <= (dateTo.HasValue ? dateTo : DateTime.Now)))
                     .Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
        }
    }

}
