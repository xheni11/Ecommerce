using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositories
{
    public class ProductRepository: BaseRepository<Product>
    {
        private const int _pageSize=10;
        private EcommerceContext _context;
        public ProductRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
        public void SetProductPublic(int id)
        {
            Product productToUpdate =  GetByID(id);
            productToUpdate.IsPublic ^= true;
             Update(productToUpdate);
        }

        public List<ProductWithDiscountModel> GetProductsWithDiscount(int pageNumber, string? name,bool? isAvaliable,DateTime? dateFrom,DateTime? dateTo)
        {

            List<ProductWithDiscountModel> result = new List<ProductWithDiscountModel>();
              var  productDiscount = (from p in _context.Product
                                   join d in _context.Discount
                                   on p.Id equals d.ProductId
                                   into ProductDiscount
                                   from product in ProductDiscount
                                   where p.IsDeleted == false && (product.IsDeleted == false) &&
                                       (p.Name.Contains(name) || string.IsNullOrEmpty(name)) && 
                                       (p.IsPublic== isAvaliable || !isAvaliable.HasValue) &&
                                       (p.CreatedOn>=(dateFrom.HasValue?dateFrom: new DateTime(1900, 1, 1, 8, 30, 52)) &&
                                       p.CreatedOn<= (dateTo.HasValue ? dateTo : DateTime.Now))
                                    select new { p.Id,p.IsDeleted,p.IsPublic,p.Name,p.Price,p.Quantity, product.Percentage })
                                    .Skip((pageNumber - 1) * _pageSize).Take(_pageSize);

                productDiscount.ToList().ForEach(el => result.Add(new ProductWithDiscountModel { Id = el.Id, Discount = el.Percentage, IsPublic = el.IsPublic, Name = el.Name, Price = el.Price, Quantity = el.Quantity }));

            return result; 
        }

        public List<Product> GetAll(int pageNumber, string? name, bool? isAvaliable, DateTime? dateFrom, DateTime? dateTo)
        {
           return _dbSet.Where(p => p.IsDeleted == false &&
                    (p.Name.Contains(name)|| string.IsNullOrEmpty(name)) && 
                    (p.IsPublic == isAvaliable || !isAvaliable.HasValue) &&
                    (p.CreatedOn >= (dateFrom.HasValue ? dateFrom : new DateTime(1900, 1, 1, 8, 30, 52)) &&
                        p.CreatedOn <= (dateTo.HasValue ? dateTo :  DateTime.Now)))
                    .Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
        }


    }
}
