﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models.Entities;

namespace AMS.Models.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account> { }
    public interface IModuleRepository : IBaseRepository<Module> { }
}