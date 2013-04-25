using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AMS.Models.App_LocalResources;

namespace AMS.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessageResourceName = "UserName_ValidateErrorMessage", ErrorMessageResourceType = typeof(LogOn))]
        [Display(Name = "UserName_DisplayName", ResourceType = typeof(LogOn))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Password_ValidateErrorMessage", ErrorMessageResourceType = typeof(LogOn))]
        [DataType(DataType.Password)]
        [Display(Name = "Password_DisplayName", ResourceType = typeof(LogOn))]
        public string Password { get; set; }

        [Display(Name = "RememberMe_DisplayName", ResourceType = typeof(LogOn))]
        public bool RememberMe { get; set; }
    }
}