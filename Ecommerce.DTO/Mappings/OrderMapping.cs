using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.DAL.Entities;

namespace Ecommerce.DTO.Mappings
{
    public static class OrderMapping
    {
        public static OrderDTO ToDTO(Order order )
        {
            return new OrderDTO
            {
                Id=order.Id,
                ProductId=order.ProductId,
                UserId=order.UserId,
                Quantity=order.Quantity
            };
        }
        public static Order ToEntityCreate(OrderDTO orderDTO)
        {
            return new Order
            {
                ProductId = orderDTO.ProductId,
                UserId = orderDTO.UserId,
                Quantity = orderDTO.Quantity,
               // CreatedBy=1,
                CreatedOn=DateTime.Now,
                IsDeleted=false                
            };
        }
        public static Order ToEntity(OrderDTO orderDTO)
        {
            return new Order
            {
                Id = orderDTO.Id,
                ProductId = orderDTO.ProductId,
                UserId = orderDTO.UserId,
                Quantity = orderDTO.Quantity
            };
        }
    }
}
