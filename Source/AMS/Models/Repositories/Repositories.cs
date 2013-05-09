using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models.Entities;
using System.Data.Objects;

namespace AMS.Models.Repositories
{
    public partial class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(ObjectContext objectContext) : base(objectContext) { }
    }

    public partial class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(ObjectContext objectContext) : base(objectContext) { }
    }
}