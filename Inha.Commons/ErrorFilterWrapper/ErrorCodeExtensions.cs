using Inha.Commons.ErrorFilterWrapper.Models;
using System;

namespace Inha.Commons.ErrorFilterWrapper
{
    public static class ErrorCodeExtensions
    {
        public static bool TryParse(string errorCodeString, out ErrorCodes errorCode)
        {
            return Enum.TryParse(errorCodeString, out errorCode);
        }

        public static ErrorCodes ToErrorCode(this string errorCodeString)
        {
            if (TryParse(errorCodeString, out ErrorCodes errorCode))
                return errorCode;

            return ErrorCodes.None;

        }
    }
}
