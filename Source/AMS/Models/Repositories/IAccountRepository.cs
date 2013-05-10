using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models.Entities;

namespace AMS.Models.Repositories
{
    public partial interface IAccountRepository
    {
        Account GetAccount(Guid id);
        Account GetAccount(string userName);
    }
}