using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace TCN.WebAPI.Providers
{
    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userId = Guid.NewGuid().ToString();
            var username = context.UserName;
            var password = context.Password;

            // Validate if user exists in your database. The next validation is to illustrate how Authentication works.
            if (!(username == "my_user" && password == "123456"))
            {
                context.SetError("invalid_grant", "The username and/or password does not exists");
            }
            else
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, userId));
                context.Validated(identity);
            }
        }
    }
}