using Inha.Commons.ErrorFilterWrapper.Models;
using System;

namespace Inha.Commons.Types
{
    public interface IBaseResponse : IDisposable
    {
        /// <summary>
        ///     Update Message Error
        /// </summary>
        /// <param name="error"></param>
        void UpdateMessageError(Error error);
    }
}
