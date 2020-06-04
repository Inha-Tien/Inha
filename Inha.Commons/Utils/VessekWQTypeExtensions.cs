using Inha.Commons.Types;
using System;

namespace Inha.Commons.Utils
{
    public static class VessekWQTypeExtensions
    {
        public static VesselWIType ToVesselWIType(this string vesselWiType)
        {

            if (Enum.TryParse(vesselWiType, out VesselWIType result))
            {
                return result;
            }
            return VesselWIType.NONE;
        }
    }
}
