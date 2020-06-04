using Castle.DynamicProxy;

namespace Inha.Commons.Log.AutoWriteLog
{
    public class DynamicProxyLog : IInterceptor
    {
        private readonly DynamicProxyAsyncLog _dynamicProxyAsyncLog;

        public DynamicProxyLog(DynamicProxyAsyncLog dynamicProxyAsyncLog)
        {
            _dynamicProxyAsyncLog = dynamicProxyAsyncLog;
        }

        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name != "Dispose")
            {
                _dynamicProxyAsyncLog.ToInterceptor()
                                     .Intercept(invocation);
            }
        }

        #endregion
    }
}
