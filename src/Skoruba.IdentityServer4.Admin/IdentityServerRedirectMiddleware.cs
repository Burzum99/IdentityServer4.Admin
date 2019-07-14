﻿using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Skoruba.IdentityServer4.Admin
{
    public class IdentityServerRedirectMiddleware
    {
        private readonly RequestDelegate _next;
        public IdentityServerRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            string location = context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location];
            Console.WriteLine(location);
        }
    }
}