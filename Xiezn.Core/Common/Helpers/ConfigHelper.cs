using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Common.Helpers
{
    public static class ConfigHelper
    {
        public static IConfiguration _configuration;

        public static string GetConfig(string key)
        {
            return _configuration[key];
        }

        public static T GetConfig<T>(string key) where T : new()
        {
            T config = new T();
            _configuration.Bind(key, config);
            return config;
        }
    }
}
