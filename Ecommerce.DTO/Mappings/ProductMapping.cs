using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Helpers;

namespace Ecommerce.DTO.Mappings
{
    public static class ProductMapping
    {
        public static ProductDTO ToDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                IsPublic=product.IsPublic,
                Name=product.Name,
                Price=product.Price,
                Quantity=product.Quantity
            };
        }

        public static ProductDTO ToDTO(ProductWithDiscountModel product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                IsPublic = product.IsPublic,
                Name = product.Name,
                DiscountPrice = product.Discount==0||product.Discount==null? product.Price:product.Price-(product.Price*(decimal)product.Discount/100),
                Price=product.Price,
                Quantity = product.Quantity
            };
        }
        public static Product ToEntityCreate(ProductDTO productDTO)
        {
            return new Product
            {
                IsPublic = productDTO.IsPublic,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity,
                // CreatedBy=1,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            };
        }
        public static Product ToEntity(ProductDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.Id,
                IsPublic = productDTO.IsPublic,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity
            };
        }
    }
}
