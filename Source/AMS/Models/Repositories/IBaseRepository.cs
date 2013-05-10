using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace AMS.Models.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : EntityObject
    {
        ObjectContext ObjectContext { get; }
        ObjectSet<TEntity> ObjectSet { get; }
    }
}