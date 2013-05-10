using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models.Entities;
using AMS.Models.Services;
using AMS.Utilities;

namespace AMS.Models.Repositories
{
    public partial class AccountRepository
    {
        public Account GetAccount(Guid id)
        {
            var result = this.ObjectSet.SingleOrDefault(i => i.ID == id);
            return result;
        }

        public Account GetAccount(string userName)
        {
            var result = this.ObjectSet.SingleOrDefault(i => i.UserName == userName);
            return result;
        }

        public IEnumerable<Role> GetRoles(string userName)
        {
            return null;
        }

        public static bool HasPermisstion(string userName, string controllerCode, string actionCode)
        {
            var result = false;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Module module = unitOfWork.ModuleRepository.GetModule(controllerCode, actionCode);
                if (module != null)
                {
                    if (module.ModuleType == (int)ModuleType.Public)
                    {
                        result = true;
                    }
                    else
                    {
                        Account account = unitOfWork.AccountRepository.GetAccount(userName);
                        if (account != null)
                        {
                            if (module.ModuleType == (int)ModuleType.Authorize)
                            {
                                result = true;
                            }
                            else if (module.ModuleType == (int)ModuleType.ModuleAuthorize)
                            {

                            }
                        }
                    }
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }
    }
}