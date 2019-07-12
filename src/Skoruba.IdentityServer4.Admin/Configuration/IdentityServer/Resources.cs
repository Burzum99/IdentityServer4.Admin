using System.Collections.Generic;
using IdentityServer4.Models;
using Skoruba.IdentityServer4.Admin.Configuration.Interfaces;

namespace Skoruba.IdentityServer4.Admin.Configuration.IdentityServer
{
    public class ClientResources
    {
        private static readonly IEnumerable<string> ClaimTypes = new List<string> { "Read", "Write", "Admin" };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("roles", "Roles", new[] {"role"}),
                new IdentityResource()
                {
                Name = "Read",
                    UserClaims = new List<string>()
                    {
                        "Read"
                    }
                },
                new IdentityResource()
                {
                    Name = "Write",
                    UserClaims = new List<string>()
                    {
                        "Write"
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources(IAdminConfiguration adminConfiguration)
        {
            return new[]
            {
                new ApiResource
                {
                    Name = adminConfiguration.IdentityAdminApiScope,
                    Scopes = new List<Scope>
                    {
                        new Scope
                        {
                            Name = adminConfiguration.IdentityAdminApiScope,
                            DisplayName = adminConfiguration.IdentityAdminApiScope,
                            UserClaims = new List<string>
                            {
                                "role"
                            },
                            Required = true
                        }
                    }
                },
                new ApiResource
                {
                    Name = "api1",
                    Description = "My API",
                    // secret for using introspection endpoint
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // include the following using claims in access token (in addition to subject id)
                    UserClaims = { "role" },
                    // this API defines two scopes
                    Scopes = new List<Scope>
                    {
                        new Scope("api1", ClaimTypes),
                    },
                }
            };
        }
    }
}