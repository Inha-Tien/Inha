using Inha.Commons.ErrorFilterWrapper.Models;
using System;

namespace Inha.Commons.Types
{
    public class BaseResponse<T> : Error, IBaseResponse
    {
        #region Properties

        /// <summary>
        ///     Data information type generic template
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///     Flag error
        /// </summary>
        public bool IsSuccess { get; set; }

        public string CheckSum { get; set; }
        public DateTime UtcDate { get; set; }
        #endregion

        #region C'tor

        /// <summary>
        ///     C'tor TResponse(T data)
        /// </summary>
        /// <param name="data"></param>
        public BaseResponse(T data) : base(string.Empty, string.Empty)
        {
            Data = data;
            IsSuccess = true;
        }

        /// <summary>
        ///     C'tor TResponse(T data, bool isError, string message, string errorCode)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public BaseResponse(T data, bool isSuccess, string message, string errorCode) : base(errorCode, message)
        {
            Data = data;
            IsSuccess = isSuccess;
        }

        #endregion



        #region public methods

        /// <summary>
        ///     Update Message Error
        /// </summary>
        /// <param name="error"></param>
        public void UpdateMessageError(Error error)
        {
            this.IsSuccess = false;
            Update(error);
        }

        /// <summary>
        /// Cập nhật mã lỗi
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public void UpdateMessageError(string message, string errorCode)
        {
            this.IsSuccess = false;
            Update(new Error(errorCode, message));
        }

        /// <summary>
        ///     Update data
        /// </summary>
        /// <param name="data"></param>
        public void UpdateData(T data)
        {
            Data = data;
        }



        #endregion
        #region IDispose pattern

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                Data = default(T);
                IsSuccess = default(bool);
            }

            _disposed = true;
        }

        #endregion
    }
}
