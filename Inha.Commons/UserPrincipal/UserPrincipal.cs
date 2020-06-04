using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using Newtonsoft.Json;

namespace Inha.Commons.UserPrincipal
{
    public class UserPrincipal : IPrincipal
    {
        public int UserId { get; private set; }

        public List<int> Roles { get; private set; }

        public string Username { get; private set; }

        #region IPrincipal Members

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public IIdentity Identity { get; }

        public UserPrincipal(string accessToken)
        {
            SetData(accessToken);
        }

        public void SetData(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                UserId = 0;
                Username = string.Empty;
                Roles = new List<int>();
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(accessToken) as JwtSecurityToken;

                Roles = new List<int>();
                if (jwtToken != null)
                {
                    UserId = Convert.ToInt32(jwtToken.Claims.First(claim => claim.Type == "sub")
                                                     ?.Value ?? "0");
                    Username = jwtToken.Claims.FirstOrDefault(c => c.Type == "Username")
                                       ?.Value ?? string.Empty;
                    string roles = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role")
                                           ?.Value ?? string.Empty;
                    if (!string.IsNullOrEmpty(roles))
                    {
                        Roles.AddRange(JsonConvert.DeserializeObject<IEnumerable<int>>(roles));
                    }
                }
            }
        }

        #endregion
    }
}
