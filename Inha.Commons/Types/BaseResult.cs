using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Inha.Commons.ErrorFilterWrapper;
using Inha.Commons.ErrorFilterWrapper.Models;
using Inha.Commons.Log.Utils;
using Inha.Commons.Log.Extensions;

namespace Inha.Commons.Types
{
    public class BaseResult
    {
        protected readonly IOptions<ErrorResourceSettings> _errorResourceSettings;

        /// <summary>
        ///     C'tor
        /// </summary>
        /// <param name="errorResourceSettings"></param>
        public BaseResult(IOptions<ErrorResourceSettings> errorResourceSettings)
        {
            _errorResourceSettings = errorResourceSettings;
        }

        public BaseResult(IConfiguration config)
        {
            try
            {
                _errorResourceSettings = Options.Create(config.GetSection("ErrorResourceSettings")
                                                              .Get<ErrorResourceSettings>());

                var t = string.Empty;
            }
            catch (Exception ex)
            {
                SerilogHelper.GetLogger()
                             ?.Error(ex.ToString());
                // write log 
            }
        }

        /// <summary>
        ///     Ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="checkSum"></param>
        /// <param name="utcDateLastUpdated"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Ok<T>(T data,
                                           string message = "",
                                           string errorCode = "",
                                           string checkSum = "",
                                           DateTime utcDateLastUpdated = default(DateTime))
        {
            return Task.FromResult(new TResponse<T>(data, true, message, errorCode, checkSum, utcDateLastUpdated));
        }

        /// <summary>
        ///     Ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Ok<T>(TResponse<T> data)
        {
            return Task.FromResult(data);
        }

        /// <summary>
        ///     Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Exception<T>(Exception ex, string sql = "", string data = "")
        {
            var res = new TResponse<T>(default(T),
                                       false,
                                       string.Format(_errorResourceSettings.InternalException500()
                                                                           .Message, ex),
                                       _errorResourceSettings.InternalException500()
                                                             .ErrorCode);
            SerilogHelper.GetLogger()
                         ?.Error(sql + "======" + data + "=======" + res.ToJson());

            return Task.FromResult(res);
        }

        /// <summary>
        ///     Fail
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Fail<T>(string message,
                                             string errorCode)
        {
            var res = new TResponse<T>(default(T), false, message, errorCode);
            SerilogHelper.GetLogger()
                         ?.Error(res.ToJson());
            return Task.FromResult(res);
        }

        /// <summary>
        ///     Fail
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Fail<T>(Error error)
        {
            var res = new TResponse<T>(default(T), false, error.Message, error.ErrorCode);
            SerilogHelper.GetLogger()
                         ?.Error(res.ToJson());
            return Task.FromResult(res);
        }

        /// <summary>
        ///     Fail
        /// </summary>
        /// <param name="error"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected Task<TResponse<T>> Fail<T>(Error error,
                                             params string[] message)
        {
            var res = new TResponse<T>(default(T), false, string.Format(error.ErrorCode, message), error.ErrorCode);
            SerilogHelper.GetLogger()
                         ?.Error(res.ToJson());
            return Task.FromResult(res);
        }

        protected Task<TResponse<T>> NotFound<T>(Error error,
                                                 params string[] message)
        {
            var res = new TResponse<T>(default(T), true, string.Format(error.ErrorCode, message), error.ErrorCode);
            SerilogHelper.GetLogger()
                         ?.Error(res.ToJson());
            return Task.FromResult(res);
        }
    }
}
