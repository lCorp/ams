using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models.Entities;

namespace AMS.Models.Repositories
{
    public partial interface IModuleRepository
    {
        Module GetModule(Guid id);
        Module GetModule(string moduleCode);
        Module GetModule(string controllerCode, string actionCode);
        IEnumerable<Module> GetModulesFor(Guid? parentId, string userName);
    }
}