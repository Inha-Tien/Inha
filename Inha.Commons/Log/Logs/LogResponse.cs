using System.Runtime.Serialization;

namespace Inha.Commons.Log.Logs
{
    [DataContract]
    public class LogResponse
    {
        public string Message { get; set; }

        /// <summary>
        /// Log type: None, BugLogic, Exception & Trace
        /// </summary>
        [DataMember]
        public LogTypes LogType { get; set; }

        /// <summary>
        /// TransactionId is code generated from client
        /// </summary>
        [DataMember]
        public string TransactionId { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        public LogResponse()
        {
            this.Message = string.Empty;
            this.LogType = LogTypes.Trace;
            this.TransactionId = string.Empty;
            this.ErrorCode = string.Empty;
        }
    }
}
