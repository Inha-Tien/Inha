using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Inha.Commons.Swaggers
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// AddSwagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="versionInfos"></param>
        /// <param name="scopes"></param>
        /// <param name="xmlFile"></param>
        /// <param name="urlIds"></param>
        public static void AddSwagger(this IServiceCollection services,
                                      IDictionary<string, Info> versionInfos,
                                      IDictionary<string, string> scopes,
                                      string xmlFile)
        {
            services.AddSwaggerGen(options =>
                                   {
                                       if (versionInfos != null)
                                       {
                                           foreach (var versionInfo in versionInfos)
                                           {
                                               options.SwaggerDoc(versionInfo.Key, versionInfo.Value);
                                           }
                                       }

                                       if (!string.IsNullOrEmpty(xmlFile))
                                       {
                                           var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                                           options.IncludeXmlComments(xmlPath);
                                       }

                                       options.DocInclusionPredicate((docName,
                                                                      apiDesc) =>
                                                                     {
                                                                         var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                                                                         // would mean this action is unversioned and should be included everywhere
                                                                         if (actionApiVersionModel == null)
                                                                         {
                                                                             return true;
                                                                         }

                                                                         if (actionApiVersionModel.DeclaredApiVersions.Any())
                                                                         {
                                                                             return actionApiVersionModel
                                                                                    .DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                                                                         }

                                                                         return actionApiVersionModel
                                                                                .ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                                                                     });
                                       //if (!string.IsNullOrEmpty(urlIds))
                                       //{
                                       //    options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                                       //    {
                                       //        Flow = "implicit",
                                       //        AuthorizationUrl = $"{urlIds}/connect/authorize",
                                       //        Scopes = scopes
                                       //    });
                                       //}

                                       options.OperationFilter<ApiVersionOperationFilter>();
                                    //   options.OperationFilter<AuthorizeCheckOperationFilter>();
                                   });
            services.AddApiVersioning(o =>
                                      {
                                          //o.Conventions.Controller
                                          o.DefaultApiVersion = new ApiVersion(1, 0); // specify the default api version
                                          o.AssumeDefaultVersionWhenUnspecified =
                                                  true; // assume that the caller wants the default version if they don't specify
                                          o.ApiVersionReader = new MediaTypeApiVersionReader(); // read the version number from the accept header
                                          o.UseApiBehavior = false;
                                          o.ReportApiVersions = true;
                                          //o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                                      });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app,
                                               string swaggerClientId,
                                               string swaggerDesc,
                                               IDictionary<string, string> endpoints)
        {
            app.UseSwagger();
            // Swagger UI
            app.UseSwaggerUI(options =>
                             {
                                 foreach (var endpoint in endpoints)
                                 {
                                     options.SwaggerEndpoint(endpoint.Key, endpoint.Value);
                                 }
                                 //options.OAuthClientId(swaggerClientId);
                                 //options.OAuthAppName(swaggerDesc);
                                 options.RoutePrefix = string.Empty;
                             });
        }
    }
}
