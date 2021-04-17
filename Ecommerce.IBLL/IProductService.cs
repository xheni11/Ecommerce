using Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.IBLL
{
    public interface IProductService
    {
        List<ProductDTO> GetAll(int pageNumber, string? name, bool? isAvaliable, DateTime? dateFrom, DateTime? dateTo, bool isForAdmin);
        ProductDTO Create(ProductDTO productDTO);
        void Update(ProductDTO productDTO);
        void SetProductToPublic(ProductDTO productDTO);
        List<ProductDTO> GetProductsWithDiscount(int pageNumber, string? name, bool? isAvaliable, DateTime? dateFrom, DateTime? dateTo, bool isForAdmin);
        void Delete(int id);
        ProductDTO GetById(int id);
    }
}
