using IdentityServer4.Models;
using System.Collections.Generic;

namespace Web.Auth.Protocol.Resources
{
    public class Identity
    {
        public static IEnumerable<IdentityResource> All => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }
}
