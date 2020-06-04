using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Inha.Commons.UserPrincipal
{
    public interface IUserPrincipalService
    {
        UserPrincipal GetUserWeb();

        UserPrincipal GetUserApi();
    }

    public class UserPrincipalService : IUserPrincipalService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string _token;

        private UserPrincipal _user;

        public UserPrincipalService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region IUserPrincipalService Members

        public UserPrincipal GetUserWeb()
        {
            try
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

                    var token = identity?.Claims.FirstOrDefault(c => c.Type == "Token");
                    if (token != null)
                    {
                        try
                        {
                            var userToken = JsonConvert.DeserializeObject<LogonResponse>(token.Value);
                            if (userToken != null)
                            {
                                string accessToken = userToken.Token?.AccessToken ?? string.Empty;
                                if (accessToken != _token)
                                {
                                    _token = accessToken;
                                    if (_user == null)
                                    {
                                        _user = new UserPrincipal(accessToken);
                                    }
                                    else
                                    {
                                        _user.SetData(accessToken);
                                    }
                                }

                                return _user;
                            }
                        }
                        catch (Exception exception)
                        {
                            //
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //
            }

            return null;
        }

        public UserPrincipal GetUserApi()
        {
            try
            {
                string accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"]
                                                         .ToString()
                                                         .Replace("Bearer ", "");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    if (accessToken != _token)
                    {
                        _token = accessToken;
                        if (_user == null)
                        {
                            _user = new UserPrincipal(accessToken);
                        }
                        else
                        {
                            _user.SetData(accessToken);
                        }
                    }

                    return _user;
                }

                return null;
            }
            catch (Exception exception)
            {
                //
            }

            return null;
        }

        #endregion
    }
}
