using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Provider
{
    public class WebApiProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string userName = "TaxServiceClient";
            string password = "5FnXmqrNaPLxNBDh";
            if (context.UserName == userName && context.Password == password)
            {
                var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                claimsIdentity.AddClaim(new Claim("admin", context.UserName)); 
                //claimsIdentity.AddClaim(new Claim("TaxServiceClientId", context.ClientId));
                await Task.Run(() => context.Validated(claimsIdentity));
            }
            else
                context.Rejected();
        }

    }
}