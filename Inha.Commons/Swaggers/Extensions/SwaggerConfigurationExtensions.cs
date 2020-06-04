using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;

namespace Inha.Commons.Swaggers.Extensions
{
    public static class SwaggerConfigurationExtensions
    {
        /// <summary>
        /// Lấy config version api
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IDictionary<string, Info> ConfigApiVersions(this IConfiguration configuration)
        {
            return configuration
                .GetSection("ApiVersions")
                .Get<Info[]>()
                .ToDictionary(p => p.Version, p => p);
        }

        /// <summary>
        /// ConfigDocumentSwagger
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ConfigDocumentSwagger(this IConfiguration configuration)
        {
            return configuration
                .GetSection("SwaggerDocs")
                .Get<DocumentSwagger[]>()
                .ToDictionary(p => p.Url, p => p.DocumentName);
        }
    }
    public class DocumentSwagger
    {
        public string Url { get; set; }
        public string DocumentName { get; set; }
    }
}
