using System;

namespace Inha.Commons.Log.Utils
{
    public class TimeUUID
    {
        private static ulong _identity = 0;
        public static string Next()
        {
            var dt = DateTime.UtcNow;
            if (_identity == 0)
            {
                _identity = ulong.Parse(dt.ToString("hhmmssffff")) + 1;
            }
            else
            {
                _identity++;
            }
            return dt.ToString("yyyyMMddhhmmssffff") + _identity.ToString();

        }
    }
}
