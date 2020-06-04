using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Inha.Commons.Configurations;
using Inha.Commons.Types;

namespace Inha.Commons.Dapper
{
    /// <summary>
    ///     DapperRepository
    /// </summary>
    public class DapperWriteRepository : BaseResult,
                                         IDapperWriteRepository
    {
        #region Define

        private readonly IConfiguration _config;

        #endregion

        #region C'tor

        /// <summary>
        ///     DapperRepository
        /// </summary>
        /// <param name="config"></param>
        public DapperWriteRepository(IConfiguration config)
                : base(config)
        {
            _config = config;
        }

        #endregion

        public IDbConnection Connection => new SqlConnection(_config.GetEx("ConnectionStrings:SqlConnectionString"));

        #region IDapperWriteRepository Members

        /// <summary>
        ///     ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TResponse<int>> ExecuteAsync(string sql,
                                                       object data)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();
                    int response = 0;
                    using (var tran = conn.BeginTransaction())
                    {
                        response = await conn.ExecuteAsync(sql, data, tran);

                        tran.Commit();
                    }

                    conn.Close();

                    return await Ok(response);
                }
            }
            catch (Exception ex)
            {
                return await Exception<int>(ex, sql);
            }
        }

        /// <summary>
        ///     ExecuteScalarAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TResponse<T>> ExecuteScalarAsync<T>(string sql,
                                                              object data)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();
                    T response = default;
                    using (var tran = conn.BeginTransaction())
                    {
                        response = await conn.ExecuteScalarAsync<T>(sql, data, tran);

                        tran.Commit();
                    }

                    conn.Close();

                    return await Ok(response);
                }
            }
            catch (Exception ex)
            {
                return await Exception<T>(ex, sql);
            }
        }

        #endregion

        #region Dispose

        // Flag: Has Dispose already been called?
        bool disposed;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                //_db.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        /// <summary>
        ///     Distroy
        /// </summary>
        ~DapperWriteRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
