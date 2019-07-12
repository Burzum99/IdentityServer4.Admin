using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Skoruba.IdentityServer4.Admin.Api.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Route("api/[controller]")]
        public async Task<ActionResult<TokenResponse>> GetToken(string clientId, string secret, string username, string password, string scope)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "http://sts.test:88",
                Policy =
               {
                   ValidateIssuerName = false,
                   RequireHttps = false
               }
            });
            var tokenResponsePassword = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = secret,
                UserName = username,
                Password = password,
                Scope = scope
            });
            return Ok(tokenResponsePassword.Json);
        }
    }
}