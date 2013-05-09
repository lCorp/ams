using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models.Entities;

namespace AMS.Models.Repositories
{
    public partial class ModuleRepository
    {
        public List<Module> GetSubModulesFor(Guid parentId, string userName)
        {
            var result = this.ObjectSet.Where(i => i.ID == parentId);
            return result.ToList();
        }
    }
}