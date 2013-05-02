using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace AMS.Models.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? skip = null, int? take = null);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void RealDelete(TEntity entity);
        void BatchInsert(IEnumerable<TEntity> entities);
        void BatchUpdate(IEnumerable<TEntity> entities);
        void BatchDelete(IEnumerable<TEntity> entities);
        void BatchRealDelete(IEnumerable<TEntity> entities);
    }
}