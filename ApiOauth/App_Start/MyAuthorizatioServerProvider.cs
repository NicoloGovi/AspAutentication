using ApiOauth.Models;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApiOauth.App_Start
{
    public class MyAuthorizatioServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var DbContext = new UsersContext())
            {

                User u = DbContext.Users.FirstOrDefault(q => q.Username.ToLower() == context.UserName.ToLower());
                if (u == null)
                {
                    context.SetError("invalid_grant", "Provided username or password is incorrect");
                    return;
                }

                if (u.Password != context.Password)
                {
                    context.SetError("invalid_grant", "Provided username or password is incorrect");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("username", u.Username));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()));
                context.Validated(identity);
            }
        }
    }
}
