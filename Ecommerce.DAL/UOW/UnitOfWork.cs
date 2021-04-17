using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Ecommerce.DAL.UOW
{
    public  class UnitOfWork
    {
        private readonly EcommerceContext _context;
        public UnitOfWork(EcommerceContext context)
        {
            _context = context;
        }
        #region ProductRepository Repository

        private ProductRepository _productRepository;

        public ProductRepository ProductRepository =>
            _productRepository ?? (_productRepository = new ProductRepository(_context));

        #endregion
        #region OrderRepository Repository

        private OrderRepository _orderRepository;

        public OrderRepository OrderRepository =>
            _orderRepository ?? (_orderRepository = new OrderRepository(_context));

        #endregion
        #region DiscountRepository Repository

        private DiscountRepository _discountRepository;

        public DiscountRepository DiscountRepository =>
            _discountRepository ?? (_discountRepository = new DiscountRepository(_context));

        #endregion

    }
}
