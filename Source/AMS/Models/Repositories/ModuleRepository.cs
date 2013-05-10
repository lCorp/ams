using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models.Entities;
using AMS.Models.Services;

namespace AMS.Models.Repositories
{
    public partial class ModuleRepository
    {
        public IEnumerable<Module> GetModulesFor(Guid? parentId, string userName)
        {
            var result = this.ObjectSet.AsEnumerable();
            if (parentId.HasValue)
            {
                result = result.Where(i => i.ParentID == parentId);
            }
            return result;
        }

        public Module GetModule(Guid id)
        {
            var result = this.ObjectSet.SingleOrDefault(i => i.ID == id);
            return result;
        }

        public Module GetModule(string controllerCode, string actionCode)
        {
            string moduleCode = controllerCode + "_" + actionCode;
            var result = this.GetModule(moduleCode);
            return result;
        }

        public Module GetModule(string moduleCode)
        {
            var result = this.ObjectSet.SingleOrDefault(i => i.ModuleCode == moduleCode);
            return result;
        }
    }
}