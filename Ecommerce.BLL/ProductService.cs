using Ecommerce.Common.Exception;
using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Helpers;
using Ecommerce.DAL.UOW;
using Ecommerce.DTO;
using Ecommerce.DTO.Mappings;
using Ecommerce.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.BLL
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _internalUnitOfWork;

        public ProductService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }
        public ProductDTO Create(ProductDTO productDTO)
        {
            Product returnedProduct =  _internalUnitOfWork.ProductRepository.Insert(ProductMapping.ToEntityCreate(productDTO));
            return  ProductMapping.ToDTO(returnedProduct);
        }

        public  void Delete(int id)
        {
            Product returnedProduct = _internalUnitOfWork.ProductRepository.GetByID(id);
             _internalUnitOfWork.ProductRepository.SoftDelete(returnedProduct);

        }


        public  List<ProductDTO> GetAll(int pageNumber, string? name, bool? isAvaliable, DateTime? dateFrom, DateTime? dateTo,bool isForAdmin)
        {

            IEnumerable<Product> products;
            if (!isForAdmin) {
                products= _internalUnitOfWork.ProductRepository.GetAll(pageNumber, name, isAvaliable, dateFrom, dateTo).ToList().Where(p => p.IsPublic);
            }
            else
            {
                products= _internalUnitOfWork.ProductRepository.GetAll(pageNumber, name, isAvaliable, dateFrom, dateTo);

            }
            return products.Select(p => ProductMapping.ToDTO(p)).ToList();
        }

        public List<ProductDTO> GetProductsWithDiscount(int pageNumber, string? name, bool? isAvaliable, DateTime? dateFrom, DateTime? dateTo, bool isForAdmin)
        {
            List<ProductWithDiscountModel> products = new List<ProductWithDiscountModel>();
            if (!isForAdmin)
            {
                products = _internalUnitOfWork.ProductRepository.GetProductsWithDiscount(pageNumber, name, isAvaliable, dateFrom,dateTo).Where(p =>p.IsPublic).ToList() ;
            }
            else
            {
                products = _internalUnitOfWork.ProductRepository.GetProductsWithDiscount(pageNumber, name, isAvaliable, dateFrom, dateTo);
            }
            return products.Select(p => ProductMapping.ToDTO(p)).ToList();
        }

        public void  SetProductToPublic(ProductDTO productDTO)
        {
             _internalUnitOfWork.ProductRepository.SetProductPublic(productDTO.Id);
        }

        public  void Update(ProductDTO productDTO)
        {
             _internalUnitOfWork.ProductRepository.Update(ProductMapping.ToEntity(productDTO));
        }
        public  ProductDTO GetById(int id)
        {
            Product product =  _internalUnitOfWork.ProductRepository.GetByID(id);
            return ProductMapping.ToDTO(product);
        }
    }
}
