using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DTO;
namespace Ecommerce.IBLL
{
    public interface IDiscountService
    {
       DiscountDTO Create(DiscountDTO discount);
        List<DiscountDTO> GetDiscountsPyProduct(ProductDTO product);
        void Delete(int id);

    }
}
