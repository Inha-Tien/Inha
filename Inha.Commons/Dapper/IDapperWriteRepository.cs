using Inha.Commons.Types;
using System;
using System.Threading.Tasks;

namespace Inha.Commons.Dapper
{
    /// <summary>
    /// IDapperWriteRepository
    /// </summary>
    public interface IDapperWriteRepository : IDisposable
    {
        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<TResponse<int>> ExecuteAsync(string sql, object data);
        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<TResponse<T>> ExecuteScalarAsync<T>(string sql, object data);
    }
}
