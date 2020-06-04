using Microsoft.Extensions.Configuration;
using Inha.Commons.Configurations;

namespace Inha.Commons.IDS.Extensions
{
    /// <summary>
    ///     IDSConfigExtensions
    /// </summary>
    public static class IDSConfigExtensions
    {
        /// <summary>
        ///     IDSConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IDSConfig IDSConfiguration(this IConfiguration configuration)
        {
            var idsConfig = configuration.GetSection("IDSConfigs")
                                .Get<IDSConfig>();
            idsConfig.URLIDS = configuration.GetEx("IDSConfigs:URLIDS");
            idsConfig.APIName = configuration.GetEx("IDSConfigs:APIName");
            idsConfig.APISwaggerName = configuration.GetEx("IDSConfigs:APISwaggerName");
            return idsConfig;
        }
    }

    public class IDSConfig
    {
        public string APIName { get; set; }

        public string URLIDS { get; set; }

        public string APIDesc { get; set; }

        public string APISwaggerName { get; set; }

        public string APISwaggerDesc { get; set; }
    }
}
