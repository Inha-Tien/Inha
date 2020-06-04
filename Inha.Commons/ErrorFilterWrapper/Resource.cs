using Microsoft.Extensions.Options;
using Inha.Commons.ErrorFilterWrapper.Models;
using System;
using System.Linq;

namespace Inha.Commons.ErrorFilterWrapper
{
    /// <summary>
    ///     Resource
    ///     <para>ErrorCodes</para>
    ///     <para>==========Error format==========</para>
    ///     <para>1XX – Informational</para>
    ///     <para>2XX – Success</para>
    ///     <para>3XX – Redirection</para>
    ///     <para>4XX – Client Error</para>
    ///     <para>5XX – Server Error</para>
    ///     <para>================================</para>
    ///     <para>Thus, My error code will follow format y the same: [xxx][yyy]</para>
    /// </summary>
    public static class ErrorResourceExtension
    {
        #region Define local variables
        private static Error _internalException500;
        private static Error _notExecute500100;
        private static Error _dataInvalid500200;

        private static Error _paramsInvalid400000;

        private static Error _dataNotAsync200000;
        private static Error _dataNotFound200100;


        #endregion

        #region support methods
        private static Error GetValue(IOptions<ErrorResourceSettings> errorResourceSetting, ErrorCodes errorCodeTypes)
        {
            var errorCode = ToCode(errorCodeTypes);
            var error = errorResourceSetting.Value.Errors
                .Where(p => p.ErrorCode.Equals(errorCode, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (error is null) return default(Error);
            return error.ToError();
        }
        private static string ToCode(ErrorCodes error)
        {
            return ((int)error).ToString();
        }

        #endregion

        /// <summary>
        /// Internal Exception (500)
        /// </summary>
        /// <param name="errorResourceSettings"></param>
        /// <returns></returns>
        public static Error InternalException500(this IOptions<ErrorResourceSettings> errorResourceSettings)
        {
            if (_internalException500 is null)
            {
                _internalException500 = GetValue(errorResourceSettings, ErrorCodes.InternalException);
            }
            return _internalException500;
        }

        /// <summary>
        /// Not Execute (500100)
        /// </summary>
        /// <param name="errorResourceSettings"></param>
        /// <returns></returns>
        public static Error NotExecute500100(this IOptions<ErrorResourceSettings> errorResourceSettings)
        {
            if (_notExecute500100 is null)
            {
                _notExecute500100 = GetValue(errorResourceSettings, ErrorCodes.NotExecute);
            }
            return _notExecute500100;
        }

        /// <summary>
        /// Data invalid (500200)
        /// </summary>
        /// <param name="errorResourceSettings"></param>
        /// <returns></returns>
        public static Error DataInvalid500200(this IOptions<ErrorResourceSettings> errorResourceSettings)
        {
            if (_dataInvalid500200 is null)
            {
                _dataInvalid500200 = GetValue(errorResourceSettings, ErrorCodes.ParameterInvalid);
            }
            return _dataInvalid500200;
        }

        /// <summary>
        /// Parameter Invalid (400000)
        /// </summary>
        /// <param name="errorResourceSettings"></param>
        /// <returns></returns>
        public static Error ParamsInvalid400000(this IOptions<ErrorResourceSettings> errorResourceSettings)
        {
            if (_paramsInvalid400000 is null)
            {
                _paramsInvalid400000 = GetValue(errorResourceSettings, ErrorCodes.DataInvalid);
            }
            return _paramsInvalid400000;
        }

        /// <summary>
        /// Data not found (404100)
        /// </summary>
        /// <param name="errorResourceSettings"></param>
        /// <returns></returns>
        public static Error DataNotFound200100(this IOptions<ErrorResourceSettings> errorResourceSettings)
        {
            if (_dataNotFound200100 is null)
            {
                _dataNotFound200100 = GetValue(errorResourceSettings, ErrorCodes.DataNotFound);
            }
            return _dataNotFound200100;
        }
        public static Error DataNotSync200000(this IOptions<ErrorResourceSettings> errorResourceSettings)
        {
            if (_dataNotAsync200000 is null)
            {
                _dataNotAsync200000 = GetValue(errorResourceSettings, ErrorCodes.DataNotSync);
            }
            return _dataNotAsync200000;
        }

    }


}

