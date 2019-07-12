using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Skoruba.IdentityServer4.Admin.Configuration.Interfaces;

namespace Skoruba.IdentityServer4.Admin.Configuration.IdentityServer

{
    public class Clients
    {
        private static readonly ICollection<Claim> Claims = new List<Claim>
        {
            new Claim("Read", "Value1"),
            new Claim("Write", "Value2"),
            new Claim("Admin", "Value3")
        };

        public static IEnumerable<Client> GetAdminClient(IAdminConfiguration adminConfiguration)
        {

            return new List<Client>
            {

	            ///////////////////////////////////////////
	            // Skoruba.IdentityServer4.Admin Client
	            //////////////////////////////////////////
	            new Client
                {

                    ClientId =adminConfiguration.ClientId,
                    ClientName =adminConfiguration.ClientId,
                    ClientUri = adminConfiguration.IdentityAdminBaseUrl,

                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret(adminConfiguration.ClientSecret.ToSha256())
                    },

                    RedirectUris = { $"{adminConfiguration.IdentityAdminBaseUrl}/signin-oidc"},
                    FrontChannelLogoutUri = $"{adminConfiguration.IdentityAdminBaseUrl}/signout-oidc",
                    PostLogoutRedirectUris = { $"{adminConfiguration.IdentityAdminBaseUrl}/signout-callback-oidc"},
                    AllowedCorsOrigins = { adminConfiguration.IdentityAdminBaseUrl },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = adminConfiguration.IdentityAdminApiSwaggerUIClientId,
                    ClientName = adminConfiguration.IdentityAdminApiSwaggerUIClientId,
                    
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = new List<string>
                    {
                        adminConfiguration.IdentityAdminApiSwaggerUIRedirectUrl
                    },
                    AllowedScopes =
                    {
                        adminConfiguration.IdentityAdminApiScope
                    },
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    RequireConsent = false,
                    Claims = Claims,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}