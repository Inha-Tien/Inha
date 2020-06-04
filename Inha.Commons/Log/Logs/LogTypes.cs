using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Inha.Commons.Log.Logs
{
    [JsonConverter(typeof(StringEnumConverter))]
    [DataContract]
    [Flags]
    public enum LogTypes
    {
        [EnumMember]
        None = 0,

        /// <summary>
        /// Lỗi logic
        /// </summary>
        [EnumMember]
        BugLogic,

        /// <summary>
        /// Lỗi exception
        /// </summary>
        [EnumMember]
        Exception,

        /// <summary>
        /// Truy vết
        /// </summary>
        [EnumMember]
        Trace
    }
}
