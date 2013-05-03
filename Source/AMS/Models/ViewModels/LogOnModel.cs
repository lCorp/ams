using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models.ViewModels
{
    public class LogOnModel
    {
        [Required(ErrorMessageResourceName = "UserName_ValidateErrorMessage", ErrorMessageResourceType = typeof(App_LocalResources.LogOnModel))]
        [Display(Name = "UserName_DisplayName", ResourceType = typeof(App_LocalResources.LogOnModel))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Password_ValidateErrorMessage", ErrorMessageResourceType = typeof(App_LocalResources.LogOnModel))]
        [DataType(DataType.Password)]
        [Display(Name = "Password_DisplayName", ResourceType = typeof(App_LocalResources.LogOnModel))]
        public string Password { get; set; }

        [Display(Name = "RememberMe_DisplayName", ResourceType = typeof(App_LocalResources.LogOnModel))]
        public bool RememberMe { get; set; }
    }
}