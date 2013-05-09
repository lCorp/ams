using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace AMS.Models.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityObject
    {
        private ObjectContext objectContext;
        private ObjectSet<TEntity> objectSet;

        public BaseRepository(ObjectContext objectContext)
        {
            this.objectContext = objectContext;
            this.objectSet = this.objectContext.CreateObjectSet<TEntity>();
        }

        public ObjectSet<TEntity> ObjectSet
        {
            get
            {
                return this.objectSet;
            }
        }
    }
}