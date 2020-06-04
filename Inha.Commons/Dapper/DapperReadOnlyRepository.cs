using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Inha.Commons.Configurations;
using Inha.Commons.Types;

namespace Inha.Commons.Dapper
{
    /// <summary>
    ///     DapperRepository
    /// </summary>
    public class DapperReadOnlyRepository :
            BaseResult,
            IDapperReadOnlyRepository
    {
        #region Define

        private readonly IConfiguration _config;

        #endregion

        #region C'tor

        /// <summary>
        ///     DapperRepository
        /// </summary>
        /// <param name="config"></param>
        public DapperReadOnlyRepository(IConfiguration config)
                : base(config)
        {
            _config = config;
        }

        #endregion

        public IDbConnection Connection
        {
            get { return new SqlConnection(_config.GetEx("ConnectionStrings:SqlConnectionString")); }
        }

        #region IDapperReadOnlyRepository Members

        /// <summary>
        ///     QuerySingleAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TResponse<T>> QuerySingleAsync<T>(string sql,
                                                            object data)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var response = await conn.QuerySingleAsync<T>(sql, data);

                    conn.Close();

                    return await Ok(response);
                }
            }
            catch (Exception ex)
            {
                return await Exception<T>(ex, sql);
            }
        }

        /// <summary>
        ///     QueryAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TResponse<IEnumerable<T>>> QueryAsync<T>(string sql,
                                                                   object data)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var response = Enumerable.Empty<T>();

                    using (var tran = conn.BeginTransaction())
                    {
                        response = await conn.QueryAsync<T>(sql, data, tran);

                        tran.Commit();
                    }

                    conn.Close();

                    return await Ok(response);
                }
            }
            catch (Exception ex)
            {
                return await Exception<IEnumerable<T>>(ex, sql);
            }
        }

        /// <summary>
        ///     QuerySingleOrDefaultAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TResponse<T>> QuerySingleOrDefaultAsync<T>(string sql,
                                                                     object data)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var response = await conn.QuerySingleOrDefaultAsync<T>(sql, data);

                    conn.Close();

                    return await Ok(response);
                }
            }
            catch (Exception ex)
            {
                return await Exception<T>(ex, sql, JsonConvert.SerializeObject(data));
            }
        }

        /// <summary>
        ///     QueryFirstOrDefaultAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TResponse<T>> QueryFirstOrDefaultAsync<T>(string sql,
                                                                    object data)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var response = await conn.QueryFirstOrDefaultAsync<T>(sql, data);

                    conn.Close();

                    return await Ok(response);
                }
            }
            catch (Exception ex)
            {
                return await Exception<T>(ex, sql);
            }
        }

        public async Task<TResponse<(T1, T2)>> QueryMultipleFFAsync<T1, T2>(string sql,
                                                                            object data)
        {
            try
            {
                var response = new TResponse<(T1, T2)>();

                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    using (var multi = await conn.QueryMultipleAsync(sql, data))
                    {
                        response.UpdateData((await multi.ReadFirstOrDefaultAsync<T1>(), await multi.ReadFirstOrDefaultAsync<T2>()));
                    }

                    conn.Close();

                    return await Ok(response);
                }
            }
            catch (Exception ex)
            {
                return await Exception<(T1, T2)>(ex, sql);
            }
        }

        public async Task<TResponse<(T1, IEnumerable<T2>)>> QueryMultipleFLAsync<T1, T2>(string sql,
                                                                                         object data)
        {
            try
            {
                var response = new TResponse<(T1, IEnumerable<T2>)>();
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    using (var multi = await conn.QueryMultipleAsync(sql, data))
                    {
                        response.UpdateData((await multi.ReadFirstOrDefaultAsync<T1>(), await multi.ReadAsync<T2>()));
                    }

                    conn.Close();
                    return await Ok(response);
                }
            }
            catch (Exception ex)
            {
                return await Exception<(T1, IEnumerable<T2>)>(ex, sql);
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
        ~DapperReadOnlyRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
