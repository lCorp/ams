using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;

namespace AMS.Models.Services
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        int SaveChanges(SaveOptions options);
    }
}