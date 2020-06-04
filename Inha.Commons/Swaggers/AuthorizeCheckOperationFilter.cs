﻿using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Inha.Commons.Swaggers
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        #region IOperationFilter Members

        public void Apply(Operation operation,
                          OperationFilterContext context)
        {
            var hasAuthorize =
                    context.MethodInfo.DeclaringType
                           .GetCustomAttributes(true)
                           .OfType<AuthorizeAttribute>()
                           .Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new Response
                {
                    Description = "Unauthorized"
                });
                operation.Responses.Add("403", new Response
                {
                    Description = "Forbidden"
                });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                                     {
                                             new Dictionary<string, IEnumerable<string>>
                                             {
                                                     {"oauth2", new[] {"demo_api"}}
                                             }
                                     };
            }
        }

        #endregion
    }
}
