namespace Inha.Commons.Utils
{
    using Inha.Commons.Types;
    using System;
    public static class WIStatusExtensions
    {/// <summary>
     /// Convert to WIActions
     /// </summary>
     /// <param name="wiStatusName"></param>
     /// <returns></returns>
        public static WIStatus ToWIStatus(this string wiStatusName)
        {

            if (Enum.TryParse(wiStatusName, out WIStatus result))
            {
                return result;
            }
            return WIStatus.NONE;
        }
    }
}
