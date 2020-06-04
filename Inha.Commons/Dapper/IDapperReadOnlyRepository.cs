using Inha.Commons.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inha.Commons.Dapper
{
    /// <summary>
    /// IDapperReadOnlyRepository
    /// </summary>
    public interface IDapperReadOnlyRepository : IDisposable
    {
        /// <summary>
        /// QuerySingleAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<TResponse<T>> QuerySingleAsync<T>(string sql, object data);
        /// <summary>
        /// QuerySingleOrDefaultAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<TResponse<T>> QuerySingleOrDefaultAsync<T>(string sql, object data);
        /// <summary>
        /// QueryFirstOrDefaultAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<TResponse<T>> QueryFirstOrDefaultAsync<T>(string sql, object data);
        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<TResponse<IEnumerable<T>>> QueryAsync<T>(string sql, object data);

        Task<TResponse<(T1, T2)>> QueryMultipleFFAsync<T1, T2>(string sql, object data);
        Task<TResponse<(T1, IEnumerable<T2>)>> QueryMultipleFLAsync<T1, T2>(string sql, object data);

    }
}
