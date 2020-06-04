using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestEase;

using Inha.Commons.Log.Logs;
using Inha.Commons.Log.Utils;
using Inha.Commons.Types;
using Inha.Commons.Log.Extensions;

namespace Inha.Commons.Log.AutoWriteLog
{
    public class DynamicProxyAsyncLog : IAsyncInterceptor
    {
        private readonly IConfiguration _configuration;

        public DynamicProxyAsyncLog(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region IAsyncInterceptor Members

        public void InterceptSynchronous(IInvocation invocation)
        {
            var serverIp = LogUtils.GetLocalIPAddress(AddressFamily.InterNetwork); // "192.168.1.1";
            var logType = LogTypes.None;
            var message = string.Empty;
            var errorCode = string.Empty;
            var throwException = false;
            string transactionId = invocation.Arguments.GetDefaultTransactionId();

            var watch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed(); //Intercepted method is executed here.
            }
            catch (ApiException ex)
            {
                throwException = true;
                logType = LogTypes.Exception;

                var ttosException = JsonConvert.DeserializeObject<JObject>(ex.Content);
                if (ttosException != null)
                {
                    message = ttosException["message"] + "";
                    errorCode = ttosException["code"] + "";
                }
                else
                {
                    message = ex.Message;
                }
            }
            catch (TTOSException ex)
            {
                throwException = true;
                logType = LogTypes.Exception;
                errorCode = ex.Code;
                message = ex.Message;
            }
            catch (Exception ex)
            {
                throwException = true;
                logType = LogTypes.Exception;
                message = ex.Message;
                errorCode = "R-EXCEPTION-500";
            }
            finally
            {
                watch.Stop();
            }

            if (invocation.ReturnValue is LogResponse returnObj)
            {
                logType = returnObj.LogType;
                message = returnObj.Message;
                errorCode = returnObj.ErrorCode;
                transactionId = returnObj.TransactionId;
            }
            else
            {
                if (logType == LogTypes.None)
                {
                    logType = LogTypes.Trace;
                }
            }

            var logDetails = new LogDetails
            {
                Data = new Data
                {
                    Parameters = invocation.Arguments,
                    ReturnValue = invocation.ReturnValue
                },
                Location = new Location
                {
                    Function = invocation.Method.Name,
                    ClassName = invocation.TargetType.Name,
                    Namespace = invocation.TargetType.Namespace
                },
                ErrorCode = errorCode,
                Identity = TimeUUID.Next(),
                LogType = logType,
                Message = message,
                Server = serverIp,
                TransactionId = transactionId,
                Client = LogUtils.GetClientIP(),
                Elapsed = watch.ElapsedMilliseconds,
                ExecDate = DateTime.Now
            };

            if (logDetails.LogType == LogTypes.Exception)
            {
                SerilogHelper.GetInstance(_configuration)
                             .Error(logDetails.ToJson());
            }
            else
            {
                SerilogHelper.GetInstance(_configuration)
                             .Debug(logDetails.ToJson());
            }

            if (throwException)
            {
                throw new TTOSException("500000", message);
            }
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        #endregion

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            var serverIp = LogUtils.GetLocalIPAddress(AddressFamily.InterNetwork); // "192.168.1.1";
            var logType = LogTypes.None;
            var message = string.Empty;
            var errorCode = string.Empty;
            var throwException = false;
            string transactionId = invocation.Arguments.GetDefaultTransactionId();

            var watch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
                var task = (Task)invocation.ReturnValue;
                await task;
            }
            catch (ApiException ex)
            {
                throwException = true;
                logType = LogTypes.Exception;

                var ttosException = JsonConvert.DeserializeObject<JObject>(ex.Content);
                if (ttosException != null)
                {
                    message = ttosException["message"] + "";
                    errorCode = ttosException["code"] + "";
                }
                else
                {
                    message = ex.Message;
                }
            }
            catch (TTOSException ex)
            {
                throwException = true;
                logType = LogTypes.Exception;
                errorCode = ex.Code;
                message = ex.Message;
            }
            catch (Exception ex)
            {
                throwException = true;
                logType = LogTypes.Exception;
                message = ex.Message;
                errorCode = "R-EXCEPTION-500";
            }
            finally
            {
                watch.Stop();
            }

            if (logType == LogTypes.None)
            {
                logType = LogTypes.Trace;
            }

            var logDetails = new LogDetails
            {
                Data = new Data
                {
                    Parameters = invocation.Arguments,
                    ReturnValue = string.Empty
                },
                Location = new Location
                {
                    Function = invocation.Method.Name,
                    ClassName = invocation.TargetType.Name,
                    Namespace = invocation.TargetType.Namespace
                },
                ErrorCode = errorCode,
                Identity = TimeUUID.Next(),
                LogType = logType,
                Message = message,
                Server = serverIp,
                TransactionId = transactionId,
                Client = LogUtils.GetClientIP(),
                Elapsed = watch.ElapsedMilliseconds,
                ExecDate = DateTime.Now
            };
            if (logDetails.LogType == LogTypes.Exception)
            {
                SerilogHelper.GetInstance(_configuration)
                             .Error(logDetails.ToJson());
            }
            else
            {
                SerilogHelper.GetInstance(_configuration)
                             .Debug(logDetails.ToJson());
            }

            if (throwException)
            {
                throw new TTOSException("500000", message, invocation.Arguments);
            }
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            var serverIp = LogUtils.GetLocalIPAddress(AddressFamily.InterNetwork);
            var logType = LogTypes.None;
            var message = string.Empty;
            var errorCode = string.Empty;
            var throwException = false;
            string transactionId = invocation.Arguments.GetDefaultTransactionId();
            TResult result = default(TResult);

            var watch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
                var task = (Task<TResult>)invocation.ReturnValue;
                result = await task;
            }
            catch (ApiException ex)
            {
                throwException = true;
                logType = LogTypes.Exception;

                var ttosException = JsonConvert.DeserializeObject<JObject>(ex.Content);
                if (ttosException != null)
                {
                    message = ttosException["message"] + "";
                    errorCode = ttosException["code"] + "";
                }
                else
                {
                    message = ex.Message;
                }
            }
            catch (TTOSException ex)
            {
                throwException = true;
                logType = LogTypes.Exception;
                errorCode = ex.Code;
                message = ex.Message;
            }
            catch (Exception ex)
            {
                throwException = true;
                logType = LogTypes.Exception;
                message = ex.Message;
                errorCode = "R-EXCEPTION-500";
            }
            finally
            {
                watch.Stop();
            }

            var returnObj = result as LogResponse;

            if (returnObj != null)
            {
                logType = returnObj.LogType;
                message = returnObj.Message;
                errorCode = returnObj.ErrorCode;
                transactionId = returnObj.TransactionId;
            }
            else
            {
                if (logType == LogTypes.None)
                {
                    logType = LogTypes.Trace;
                }
            }

            var logDetails = new LogDetails
            {
                Data = new Data
                {
                    Parameters = invocation.Arguments,
                    ReturnValue = result
                },
                Location = new Location
                {
                    Function = invocation.Method.Name,
                    ClassName = invocation.TargetType.Name,
                    Namespace = invocation.TargetType.Namespace
                },
                ErrorCode = errorCode,
                Identity = TimeUUID.Next(),
                LogType = logType,
                Message = message,
                Server = serverIp,
                TransactionId = transactionId,
                Client = LogUtils.GetClientIP(),
                Elapsed = watch.ElapsedMilliseconds,
                ExecDate = DateTime.Now
            };

            if (logDetails.LogType == LogTypes.Exception)
            {
                SerilogHelper.GetInstance(_configuration)
                             .Error(logDetails.ToJson());
            }
            else
            {
                SerilogHelper.GetInstance(_configuration)
                             .Debug(logDetails.ToJson());
            }

            if (!throwException)
            {
                return result;
            }

            throw new TTOSException(errorCode, message);
        }
    }
}
