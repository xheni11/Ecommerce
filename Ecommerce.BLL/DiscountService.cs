using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DAL.Entities;
using Ecommerce.DAL.UOW;
using Ecommerce.DTO;
using Ecommerce.DTO.Mappings;
using Ecommerce.IBLL;

namespace Ecommerce.BLL
{
    public class DiscountService : IDiscountService
    {
        private readonly UnitOfWork _internalUnitOfWork;

        public DiscountService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }
        public DiscountDTO Create(DiscountDTO discount)
        {
            Discount returnedDiscount= _internalUnitOfWork.DiscountRepository.Insert(DiscountMapping.ToEntityCreate(discount));
            return DiscountMapping.ToDTO(returnedDiscount);
        }

        public void  Delete(int id)
        {
            Discount returnedDiscount =  _internalUnitOfWork.DiscountRepository.GetByID(id);
             _internalUnitOfWork.DiscountRepository.SoftDelete(returnedDiscount);
        }

        public List<DiscountDTO> GetDiscountsPyProduct(ProductDTO product)
        {
            List<Discount> returnedDiscounts= _internalUnitOfWork.DiscountRepository.GetDiscountsByProduct(ProductMapping.ToEntity(product));
            return returnedDiscounts.Select(d=>DiscountMapping.ToDTO(d)).ToList();
        }
    }
}
