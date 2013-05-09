using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using AMS.Models.Entities;

namespace AMS.Models.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private ObjectContext objectContext;

        public UnitOfWork()
            : this(new AMSEntities())
        {

        }

        public UnitOfWork(ObjectContext objectContext)
        {
            this.objectContext = objectContext;
        }

        public void Dispose()
        {
            this.objectContext.Dispose();
        }
    }
}