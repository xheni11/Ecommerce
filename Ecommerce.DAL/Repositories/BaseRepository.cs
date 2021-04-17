using Ecommerce.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositories
{
    public class BaseRepository<TEntity>  where TEntity : BaseEntity, new()
    {
        internal DbContext _dbContext { get; set; }
        internal DbSet<TEntity> _dbSet;
        private const int _pageSize = 10;
        public BaseRepository(EcommerceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _dbContext = context;
            // _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return  orderBy != null ?  orderBy(query).ToList() :  query.ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return   _dbSet.Where(entity => entity.IsDeleted == false).ToList();
        }

        public TEntity GetByID(object id)
        {
            return  _dbSet.Find(id);
        }

        public  TEntity Insert(TEntity entity)
        {
            _dbSet.AddAsync(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(object id)
        {
            var entity = _dbSet.FindAsync(id);
            Delete(entity);
        }


        public  void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            _dbContext.SaveChanges();
        }

        public void  Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public  void SoftDelete(TEntity entityToDelete)
        {
            entityToDelete.IsDeleted = true;
            _dbSet.Attach(entityToDelete);
            _dbContext.Entry(entityToDelete).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public Expression<Func<T, object>> CreateExpression<T>(string propertyName)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type);
            var access = Expression.Property(parameter, property);
            var function = Expression.Lambda<Func<T, object>>(access, parameter);

            return function;
        }

        public int CountAllRecords(Expression<Func<TEntity, bool>> filter = null)
        {

            return filter != null ? _dbSet.Where(filter).Count() : _dbSet.Count();
        }

          public  IEnumerable<TEntity> GetAll( int pageNumber, string columnName, bool desc)
        {

            if (!desc)
            {
                return  _dbSet.Where(p => 
                 p.IsDeleted == false).             
                     OrderBy(CreateExpression<TEntity>(columnName)).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
            }
            else
            {
               
                return   _dbSet.Where(p =>
                 p.IsDeleted == false).
                     OrderByDescending(CreateExpression<TEntity>(columnName)).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
            }
           
        }
    }
 }
