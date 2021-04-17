using Ecommerce.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.Repositories
{
    public static class RepositoryFactory
    {
        public static BaseRepository<TEntity> CreateRepository<TEntity>(EcommerceContext dbContext)
            where TEntity : BaseEntity, new() =>
            new BaseRepository<TEntity>(dbContext);
    }
}
