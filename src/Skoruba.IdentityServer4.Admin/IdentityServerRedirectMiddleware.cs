using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Skoruba.IdentityServer4.Admin
{
    public class IdentityServerRedirectMiddleware
    {
        private readonly RequestDelegate _next;
        private bool hasRedirected;
        public IdentityServerRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
            hasRedirected = false;
        }

        public async Task Invoke(HttpContext context)
        {
            if (hasRedirected == false)
            {
                await _next(context);
                string location = context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location];
                Console.WriteLine(location);
                if (location.Contains("sts.test:88"))
                {
                    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location] = location.Replace("sts.test:88", "localhost:9000");
                }
                hasRedirected = true;
            }
        }
    }
}