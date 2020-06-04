using Inha.Commons.Types;
using System;

namespace Inha.Commons.Utils
{
    public static class KafkaExtensions
    {
        public static SenderTypes ToSenderTypes(this string senderName)
        {
            if (Enum.TryParse(senderName.ToUpper(), out SenderTypes result))
            {
                return result;
            }
            return SenderTypes.NONE;
        }

        public static ObjectTransferTypes ToObjectTransferTypes(this string objectName)
        {
            if (Enum.TryParse(objectName.ToUpper(), out ObjectTransferTypes result))
            {
                return result;
            }
            return ObjectTransferTypes.NONE;
        }
    }
}
