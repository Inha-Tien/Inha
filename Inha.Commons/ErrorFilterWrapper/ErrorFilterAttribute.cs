using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Inha.Commons.ErrorFilterWrapper.Models;
using Inha.Commons.Types;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inha.Commons.ErrorFilterWrapper
{
    public class ErrorFilterAttribute : ActionFilterAttribute
    {
        #region Defines
        private readonly IOptions<ErrorResourceSettings> _errorResourceSettings;
        private static IDictionary<string, Error> _dicError;
        #endregion

        #region C'tor
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="errorResourceSettings"></param>
        public ErrorFilterAttribute(IOptions<ErrorResourceSettings> errorResourceSettings)
        {
            _errorResourceSettings = errorResourceSettings;
        }
        #endregion


        /// <summary>
        ///     Init Dictionary Error
        /// </summary>
        private void Init()
        {
            if (_dicError is null)
            {
                _dicError = new Dictionary<string, Error>
                {
                    {_errorResourceSettings.DataNotFound200100().ErrorCode, _errorResourceSettings.DataNotFound200100()},
                    {_errorResourceSettings.InternalException500().ErrorCode, _errorResourceSettings.InternalException500()},
                    {_errorResourceSettings.NotExecute500100().ErrorCode, _errorResourceSettings.NotExecute500100()},
                    {_errorResourceSettings.ParamsInvalid400000().ErrorCode, _errorResourceSettings.ParamsInvalid400000()},
                };
            }
        }

        //private string RemoveSpecialCharacter(string input)
        //{
        //    if(!string.IsNullOrEmpty(input))
        //    {
        //        input = input.Replace("'", string.Empty)
        //                     .Replace("\"", string.Empty)
        //                     .Replace("*", string.Empty)
        //                     .Replace("\\", string.Empty)
        //                     .Replace("~", string.Empty)
        //                     .Replace("!", string.Empty)
        //                     .Replace("@", string.Empty)
        //                     .Replace("#", string.Empty)
        //                     .Replace("$", string.Empty)
        //                     .Replace("%", string.Empty)
        //                     .Replace("^", string.Empty)
        //                     .Replace("&", string.Empty)
        //                     .Replace("(", string.Empty)
        //                     .Replace(")", string.Empty)
        //                     .Replace("/", string.Empty);
        //    }

        //    return input;
        //}

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                object tmp = null;
                context.ActionArguments.TryGetValue("command", out tmp);
                if (tmp == null)
                {
                    context.ActionArguments.TryGetValue("query", out tmp);
                }
                if (tmp != null)
                {
                    StringBuilder builder = new StringBuilder();
                    string condition = string.Empty;
                    for (int i = 0; i < context.ModelState.Count; i++)
                    {
                        var attr = context.ModelState.Keys.ElementAt(i);
                        if (context.ModelState.Values.ElementAt(i).ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                        {
                            var errorCode = context.ModelState.Values.ElementAt(i)
                                                   .Errors[context.ModelState.Values.ElementAt(i)
                                                                  .Errors.Count - 1]
                                                   .ErrorMessage;

                            builder.Append($"{condition}{attr}: {errorCode}");
                            condition = "; ";
                        }


                    }

                    var response = new TResponse<object>(null, false, builder.ToString(), "500");

                    context.Result = new OkObjectResult(response);
                }
            }
            else
            {
                //object tmp = null;
                //context.ActionArguments.TryGetValue("command", out tmp);
                //if (tmp == null)
                //{
                //    context.ActionArguments.TryGetValue("query", out tmp);
                //}

                //if(tmp!=null)
                //{
                //    Type myType = tmp.GetType();
                //    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                //    foreach (PropertyInfo prop in props)
                //    {
                //        object propValue = prop.GetValue(tmp, null);
                //        if(propValue is string )
                //        {
                //            prop.SetValue(tmp, RemoveSpecialCharacter(propValue.ToString()));
                //        }
                //    }
                //}
                await next();
            }
        }
    }
}
