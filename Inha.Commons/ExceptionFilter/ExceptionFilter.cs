using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Inha.Commons.Types;

namespace Inha.Commons.ExceptionFilter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;

            var ttosException = context.Exception as TTOSException;
            if (ttosException != null)
            {
                context.Result = new JsonResult(new TTOSException(ttosException.Code));
            }
            else
            {
                context.Result = new JsonResult(context.Exception);
            }
        }
    }
}
