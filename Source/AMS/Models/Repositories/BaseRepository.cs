using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using AMS.Models.Entities;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace AMS.Models.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal AMSEntities context;
        internal ObjectSet<TEntity> objects;

        public BaseRepository(AMSEntities context)
        {
            this.context = context;
            this.objects = context.CreateObjectSet<TEntity>();
        }

        public ObjectSet<TEntity> Objects
        {
            get
            {
                return this.objects;
            }
        }

        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? skip = null, int? take = null)
        {
            IQueryable<TEntity> query = this.objects;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue && take.HasValue)
            {
                query = query.Skip(skip.Value).Take(take.Value);
            }

            return query;
        }

        public void Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (entity.GetType().GetProperty("ID").GetValue(entity, null).ToString() == Guid.Empty.ToString())
                {
                    entity.GetType().GetProperty("ID").SetValue(entity, Guid.NewGuid(), null);
                }

                this.objects.AddObject(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                ThrowDbEntityValidationException(dbEx);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                ThrowDbEntityValidationException(dbEx);
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.GetType().GetProperty("Deleted").SetValue(entity, true, null);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                ThrowDbEntityValidationException(dbEx);
            }
        }

        public void RealDelete(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.objects.DeleteObject(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                ThrowDbEntityValidationException(dbEx);
            }
        }

        public void BatchInsert(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                if (entities.Count() > 0)
                {
                    foreach (TEntity entity in entities)
                    {
                        if (entity.GetType().GetProperty("ID").GetValue(entity, null).ToString() == Guid.Empty.ToString())
                        {
                            entity.GetType().GetProperty("ID").SetValue(entity, Guid.NewGuid(), null);
                        }

                        this.objects.AddObject(entity);
                    }
                    this.context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                ThrowDbEntityValidationException(dbEx);
            }
        }

        public void BatchUpdate(IEnumerable<TEntity> entities)
        {
            try
            {

            }
            catch (DbEntityValidationException dbEx)
            {
                ThrowDbEntityValidationException(dbEx);
            }
        }

        public void BatchDelete(IEnumerable<TEntity> entities)
        {
            try
            {

            }
            catch (DbEntityValidationException dbEx)
            {
                ThrowDbEntityValidationException(dbEx);
            }
        }

        public void BatchRealDelete(IEnumerable<TEntity> entities)
        {
            try
            {

            }
            catch (DbEntityValidationException dbEx)
            {
                ThrowDbEntityValidationException(dbEx);
            }
        }

        private static void ThrowDbEntityValidationException(DbEntityValidationException dbEx)
        {
            var msg = string.Empty;

            foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

            var fail = new Exception(msg, dbEx);
            throw fail;
        }
    }
}