﻿using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Inha.Commons.Swaggers
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        #region IOperationFilter Members

        public void Apply(Operation operation,
                          OperationFilterContext context)
        {
            var actionApiVersionModel = context.ApiDescription.ActionDescriptor?.GetApiVersion();
            if (actionApiVersionModel == null)
            {
                return;
            }

            if (actionApiVersionModel.DeclaredApiVersions.Any())
            {
                operation.Produces = operation.Produces
                                              .SelectMany(p => actionApiVersionModel.DeclaredApiVersions
                                                                                    .Select(version => $"{p};v={version.ToString()}"))
                                              .ToList();
            }
            else
            {
                operation.Produces = operation.Produces
                                              .SelectMany(p => actionApiVersionModel.ImplementedApiVersions.OrderByDescending(v => v)
                                                                                    .Select(version => $"{p};v={version.ToString()}"))
                                              .ToList();
            }
        }

        #endregion
    }
}
