using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoruba.IdentityServer4.Admin
{
    public static class RequestIdentityServerRedirect
    {
        public static IApplicationBuilder UseIdentityServerRedirect(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<IdentityServerRedirectMiddleware>();
        }
    }
}
