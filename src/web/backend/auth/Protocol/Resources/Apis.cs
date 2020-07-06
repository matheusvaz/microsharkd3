using IdentityServer4.Models;
using System.Collections.Generic;

namespace Web.Auth.Protocol.Resources
{
    public class Apis
    {
        public static IEnumerable<ApiResource> All => new List<ApiResource>
        {
            new ApiResource("api", "My API")
            {
                ApiSecrets = { new Secret(Env.GetString("API_SECRET").Sha256()) },
                Scopes = {"api"}                
            }
        };

        public static IEnumerable<ApiScope> Scopes => new List<ApiScope>
        {
            new ApiScope(name: "api", displayName: "api")
        };
    }
}
