using System;

namespace Inha.Commons.Types
{
    public class TTOSException : Exception
    {
        public string Code { get; }

        public TTOSException()
        {
        }

        public TTOSException(string code)
        {
            Code = code;
        }

        public TTOSException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public TTOSException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public TTOSException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public TTOSException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code.Contains("|")
                           ? code
                           : $"{code}|{string.Format(message, args)}";
        }
    }
}
