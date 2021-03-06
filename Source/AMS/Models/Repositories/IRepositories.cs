﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models.Entities;

namespace AMS.Models.Repositories
{
    public partial interface IAccountRepository : IBaseRepository<Account> { }
    public partial interface IModuleRepository : IBaseRepository<Module> { }
}