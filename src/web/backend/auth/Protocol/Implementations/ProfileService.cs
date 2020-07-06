using Common.Domain.Multi;
using Common.Domain.ValueObject;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Membership.Application.Queries;
using Membership.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.Protocol.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly UserQueries userQueries;

        public ProfileService(UserQueries userQueries)
        {
            this.userQueries = userQueries;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await userQueries.FindUser(new Id(context.Subject.GetSubjectId()));
            var claims = new List<System.Security.Claims.Claim>();

            if (user != null)
            {
                user.Claims.ForEach(claim => claims.Add(new System.Security.Claims.Claim(claim.Key, claim.Value)));
                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await userQueries.FindUser(new Id(context.Subject.GetSubjectId()));

            if (user != null)
            {
                context.IsActive = user.Status == UserStatus.Active ? true : false;
            }
        }
    }
}
