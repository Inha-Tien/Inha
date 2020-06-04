using Inha.Commons.Types;
using System;

namespace Inha.Commons.Utils
{
    public static class WorkQueueTypeExtensions
    {
        public static WorkQueueTypes ToWorkQueueType(this string wqTypeName)
        {

            if (Enum.TryParse(wqTypeName, out WorkQueueTypes result))
            {
                return result;
            }
            return WorkQueueTypes.NONE;
        }
    }
}
