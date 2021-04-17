using Ecommerce.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.IBLL;
using Ecommerce.DTO;
using Ecommerce.DAL.Entities;
using Ecommerce.DTO.Mappings;
using System.Threading.Tasks;
using System.Linq;

namespace Ecommerce.BLL
{
    public class OrderService:IOrderService
    {
        private readonly UnitOfWork _internalUnitOfWork;

        public OrderService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
        }

        public OrderDTO Create(OrderDTO order)
        {
            Order returnedProduct = new Order();
            Product product = _internalUnitOfWork.ProductRepository.GetByID(order.ProductId);
            if (order.Quantity < product.Quantity)
            {
                product.Quantity -= order.Quantity;
                 _internalUnitOfWork.ProductRepository.Update(product);
                returnedProduct =  _internalUnitOfWork.OrderRepository.Insert(OrderMapping.ToEntityCreate(order));
            }
            else if (order.Quantity== product.Quantity)
            {
                    product.Quantity -= order.Quantity;
                    product.IsPublic = false;
                     _internalUnitOfWork.ProductRepository.Update(product);
                    returnedProduct =  _internalUnitOfWork.OrderRepository.Insert(OrderMapping.ToEntityCreate(order));
            }
            return OrderMapping.ToDTO(returnedProduct);
        }

        public  List<OrderDTO> GetAll(int pageNumber, string? name, string? userName, DateTime? dateFrom, DateTime? dateTo)
        {
            var orders =  _internalUnitOfWork.OrderRepository.GetAll(pageNumber,name, userName,dateFrom,dateTo);
            return orders.Select(p => OrderMapping.ToDTO(p)).ToList();
        }

        public  OrderDTO GetById(int id)
        {
            Order order =  _internalUnitOfWork.OrderRepository.GetByID(id);
            return OrderMapping.ToDTO(order);
        }
    }
}
