namespace Inha.Commons.Utils
{
    using Inha.Commons.Types;
    using System;
    public static class WIActionExtensions
    {
        /// <summary>
        /// Convert to WIActions
        /// </summary>
        /// <param name="wiActionName"></param>
        /// <returns></returns>
        public static WIActions ToWIActions(this string wiActionName)
        {

            if (Enum.TryParse(wiActionName, out WIActions result))
            {
                return result;
            }
            return WIActions.NONE;
        }
    }
}
