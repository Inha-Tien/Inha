using System;
using Inha.Commons.Log.Utils;

namespace Inha.Commons.Log.Logs
{
    public class LogDetails
    {
        #region Attributes

        /// <summary>
        ///     Mã định danh cho mỗi log.
        ///     Là Guid phát sinh dựa trên thời gian nên có thể sort được.
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        ///     Mã transaction phát sinh theo giao dịch, các log trong luồng giao dịch đều có chung mã transactionId.
        ///     TransactionId là Guid phát sinh dựa trên thời gian nên có thể sort được.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        ///     IP Server đang phát sinh log
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        ///     Function gọi đến(optional)
        /// </summary>
        public string Caller { get; set; }

        /// <summary>
        ///     IP Client gọi đến (optional)
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        ///     Chứa thông tin:
        ///     Namespace,
        ///     Class-name,
        ///     Function-name
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        ///     Nội dung lỗi hoặc những ghi chú của dev
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Input/output
        /// </summary>
        public Data Data { get; set; }

        public LogTypes LogType { get; set; }

        public string ErrorCode { get; set; }

        public long Elapsed { get; set; }

        public DateTime ExecDate { get; set; }

        public string Site { get; set; }

        #endregion

        #region C'tor

        public LogDetails()
        {
        }

        public LogDetails(string message, LogTypes logType) : this()
        {
            Identity = TimeUUID.Next();
            TransactionId = string.Empty;
            Server = "192.168.1.1";
            Caller = string.Empty;
            Client = LogUtils.GetClientIP();
            Location = new Location();
            Message = message;
            Data = new Data();
            LogType = logType;
            ErrorCode = string.Empty;
            Elapsed = 0;
            ExecDate = DateTime.Now;
            Site = string.Empty;
        }

        #endregion
    }
}
