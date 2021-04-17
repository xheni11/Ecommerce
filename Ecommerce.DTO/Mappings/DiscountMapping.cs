using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.DAL.Entities;
namespace Ecommerce.DTO.Mappings
{
    public static class DiscountMapping
    {
        public static DiscountDTO ToDTO(Discount discount)
        {
            return new DiscountDTO
            {
                Id = discount.Id,
                ProductId = discount.ProductId,
                UserId = discount.UserId,
                Percentage = discount.Percentage
            };
        }
        public static Discount ToEntityCreate(DiscountDTO discountDTO)
        {
            return new Discount
            {
                ProductId = discountDTO.ProductId,
                UserId = discountDTO.UserId,
                Percentage = discountDTO.Percentage,
                // CreatedBy=1,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            };
        }
        public static Discount ToEntity(DiscountDTO discountDTO)
        {
            return new Discount
            {
                Id = discountDTO.Id,
                ProductId = discountDTO.ProductId,
                UserId = discountDTO.UserId,
                Percentage = discountDTO.Percentage
            };
        }
    }
}
