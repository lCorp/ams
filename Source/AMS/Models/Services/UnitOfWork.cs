using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using AMS.Models.Entities;
using AMS.Models.Repositories;

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

        public int SaveChanges()
        {
            return this.objectContext.SaveChanges();
        }

        public int SaveChanges(SaveOptions options)
        {
            return this.objectContext.SaveChanges(options);
        }

        public void Dispose()
        {
            this.objectContext.Dispose();
        }

        private IAccountRepository accountRepository;
        public IAccountRepository AccountRepository
        {
            get
            {
                if (this.accountRepository == null)
                {
                    this.accountRepository = new AccountRepository(this.objectContext);
                }
                return this.accountRepository;
            }
        }

        private IModuleRepository moduleRepository;
        public IModuleRepository ModuleRepository
        {
            get
            {
                if (this.moduleRepository == null)
                {
                    this.moduleRepository = new ModuleRepository(this.objectContext);
                }
                return this.moduleRepository;
            }
        }
    }
}