using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace AMS.Utilities
{
    public class Configuration
    {
        /// <summary>
        /// Get app setting value by key in web.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void SetSetting(string key, string value)
        {
            ConfigurationManager.AppSettings.Set(key, value);
        }
    }
}