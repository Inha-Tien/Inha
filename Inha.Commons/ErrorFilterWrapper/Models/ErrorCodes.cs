namespace Inha.Commons.ErrorFilterWrapper.Models
{
    /// <summary>
    /// ErrorCodes:
    /// <para> None = 0</para>
    /// <para> Success = 200</para>
    /// <para> InternalException = 500</para>
    /// <para> NotExecute = 500100</para>
    /// <para> DataInvalid = 500100</para>
    /// <para> ParameterInvalid = 400000</para>
    /// <para> DataNotSync = 200000</para>
    /// <para> DataNotFound = 200100</para>
    /// </summary>
    public enum ErrorCodes
    {
        None = 0,
        Success = 200,
        InternalException = 500,
        NotExecute = 500100,
        DataInvalid = 500200,
        ParameterInvalid = 400000,
        /// <summary>
        /// Không đồng bộ data giữa OPS core và OPS Web
        /// </summary>
        DataNotSync = 200000,
        DataNotFound = 200100
    }
}
