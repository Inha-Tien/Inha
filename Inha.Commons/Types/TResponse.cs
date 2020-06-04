using System;

namespace Inha.Commons.Types
{
    /// <summary>
    /// TResponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TResponse<T> : BaseResponse<T>
    {
        public TResponse() : base(default(T))
        {

        }
        public TResponse(T data) : base(data)
        {
        }
        public TResponse(T data, bool isSuccess, string message, string errorCode, string checkSum = "", DateTime utcDateLastUpdated = default(DateTime))
           : base(data, isSuccess, message, errorCode)
        {
            this.CheckSum = checkSum;
            this.UtcDate = utcDateLastUpdated;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
