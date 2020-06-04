namespace Inha.Commons.Types
{
    public enum WIStatus
    {
        NONE = 0,
        REQUEST,
        INPROGRESS,
        DONE,
        FAIL,
        /// <summary>
        /// Tham số Pending dành riêng WITrailer
        /// </summary>
        PENDING
    }
}
