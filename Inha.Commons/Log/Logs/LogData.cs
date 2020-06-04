using System;

namespace Inha.Commons.Log.Logs
{
    public class LogData
    {
        /// <summary>
        /// Mã giao tác. Dùng để truy các log của services thuộc về giao tác này.
        /// </summary>
        /// <example>
        /// Vd: Button 1 gọi 3 Service: Service 1, Service 2, Service 3:
        ///      + Flow Service 1: Service 1 -> Service X1 -> Adapter 1
        ///      + Flow Service 2: Service 2 -> Service X2 -> Adapter 2
        ///      + Flow Service 3: Service 3 -> Service X3 -> Adapter 3
        ///      
        /// - Mỗi Service và Adapter là nơi cần ghi log, và tất cả đều có cùng TransactionId
        /// </example>
        public string TransactionId { get; set; }

        /// <summary>
        /// Địa chỉ của Server đang ghi log 
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Function name
        /// </summary>
        public string Function
        {
            get
            {
                return SourceLocation.Function;
            }
        }

        /// <summary>
        /// Địa chỉ IP của Server gọi đến
        /// </summary>
        public string Caller { get; set; }

        /// <summary>
        /// Site hiện tại
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Thời gian thực thi
        /// </summary>
        public DateTime ExecDate { get; set; }

        /// <summary>
        /// Tổng chi phí
        /// </summary>
        public long ExecCost { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Mô tả lỗi
        /// </summary>
        public string ErrorDesc { get; set; }

        /// <summary>
        /// Chi tiết lỗi
        /// </summary>
        public LogDetails ErrorDetails { get; set; }

        /// <summary>
        /// Vị trí Log tại Function cha
        /// </summary>
        public Location SourceLocation { get; set; }

        /// <summary>
        /// Input/Ouput
        /// </summary>
        public Data Data { get; set; }

        /// <summary>
        /// Loại log: None, Logic, Exception, Trace
        /// </summary>
        public LogTypes LogType { get; set; }
    }
    /// <summary>
    /// Chứa thông tin input/output của 1 function
    /// </summary>
    public class Data
    {
        /// <summary>
        /// đối tượng input
        /// </summary>
        public object Parameters;
        /// <summary>
        /// đối tượng output
        /// </summary>
        public object ReturnValue;
    }
}
