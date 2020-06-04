namespace Inha.Commons.ErrorFilterWrapper.Models
{
    public class ErrorModel
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public ErrorModel()
        {
            this.ErrorCode =
                this.Message = string.Empty;
        }
        public Error ToError()
        {
            return new Error(this.ErrorCode, this.Message);
        }
    }
}
