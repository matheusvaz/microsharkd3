using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Web.Auth.Protocol
{
    public static class Clients
    {
        public static IEnumerable<Client> All => new List<Client>
        {
            new Client
            {
                ClientId = "mobile",
                ClientName = "Mobile client (Android / iOS)",
                Description = "This client is for Android and iOS apps",
                RequireConsent = false,
                RequireClientSecret = false,
                AccessTokenType = AccessTokenType.Reference,
                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api"
                }
            },

            new Client
            {
                ClientId = "interactive.confidential",
                ClientName = "Interactive client (Code with PKCE)",

                RedirectUris = { "https://localhost:4000/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:4000" },
                FrontChannelLogoutUri = "https://localhost:4000/signout",

                ClientSecrets = { new Secret("secret".Sha256()) },
                RequireConsent = false,

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = true,
                AllowedScopes = { "openid", "profile", "email", "api" },

                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.ReUse
            },
        };
    }
}
