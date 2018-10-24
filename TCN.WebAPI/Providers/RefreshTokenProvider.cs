using System;
using Microsoft.Owin.Security.Infrastructure;

namespace TCN.WebAPI.Providers
{
    public class RefreshTokenProvider : AuthenticationTokenProvider
    {
        public override void Create(AuthenticationTokenCreateContext context)
        {
            // Expiration time in seconds            
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.Now.AddSeconds(2592000));
            context.SetToken(context.SerializeTicket());
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}