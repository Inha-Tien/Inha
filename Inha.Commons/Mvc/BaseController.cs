using Microsoft.AspNetCore.Mvc;
using Inha.Commons.Dispatchers;
using Inha.Commons.Messages;
using Inha.Commons.Types;
using System.Threading.Tasks;

namespace Inha.Commons.Mvc
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IDispatcher _dispatcher;

        public BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => await _dispatcher.QueryAsync<TResult>(query);

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        protected ActionResult<PagedResult<T>> Collection<T>(PagedResult<T> pagedResult)
        {
            if (pagedResult == null)
            {
                return NotFound();
            }

            return Ok(pagedResult);
        }
        protected async Task Execute<TResult>(TResult query) where TResult : ICommand
        {
            await _dispatcher.SendAsync<TResult>(query);
        }


        private static string _token;

        private static Inha.Commons.UserPrincipal.UserPrincipal _user;

        /// <summary>
        /// UserPrincipal
        /// </summary>
        protected Inha.Commons.UserPrincipal.UserPrincipal UserPrincipal
        {
            get
            {
                return GetCurrentUserLogin();
            }
        }

        /// <summary>
        /// GetCurrentUserLogin
        /// </summary>
        /// <returns></returns>
        private Inha.Commons.UserPrincipal.UserPrincipal GetCurrentUserLogin()
        {
            string accessToken = Request.HttpContext.Request.Headers["Authorization"]
                                                         .ToString()
                                                         .Replace("Bearer ", "");
            if (string.IsNullOrEmpty(accessToken))
                return null;


            if (accessToken != _token)
            {
                _token = accessToken;
                if (_user is null)
                {
                    _user = new Inha.Commons.UserPrincipal.UserPrincipal(accessToken);
                }
                else
                {
                    _user.SetData(accessToken);
                }
            }
            return _user;

        }
    }
}
