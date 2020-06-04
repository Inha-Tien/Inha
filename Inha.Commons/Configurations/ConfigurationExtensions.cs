using System;
using Microsoft.Extensions.Configuration;
using Inha.Commons.Security;

namespace Inha.Commons.Configurations
{
    public static class ConfigurationExtensions
    {
        public static T GetEx<T>(this IConfiguration config,
                                 string key,
                                 bool security = true)
        {
            try
            {
                key = $"{config["SecurityKey"]}{key}";

                var stringRaw = security
                                        ? SecurityUtil.Decrypt(config[key], key)
                                        : config[key];

                return (T)Convert.ChangeType(stringRaw, typeof(T));
            }
            catch (Exception exception)
            {
                //
            }

            return default(T);
        }

        public static T GetEx<T>(this IConfiguration config,
                                 string value,
                                 string key,
                                 bool security = true)
        {
            try
            {
                key = $"{config["SecurityKey"]}{key}";

                var stringRaw = security
                                        ? SecurityUtil.Decrypt(value, key)
                                        : value;

                return (T)Convert.ChangeType(stringRaw, typeof(T));
            }
            catch (Exception exception)
            {
                //
            }

            return default(T);
        }

        public static string GetEx(this IConfiguration config,
                                   string key,
                                   bool security = true)
        {
            key = $"{config["SecurityKey"]}{key}";

            return config.GetEx<string>(key, security);
        }

        public static string GetEx(this IConfiguration config,
                                   string value,
                                   string key,
                                   bool security = true)
        {
            key = $"{config["SecurityKey"]}{key}";

            return config.GetEx<string>(value, key, security);
        }
    }
}
