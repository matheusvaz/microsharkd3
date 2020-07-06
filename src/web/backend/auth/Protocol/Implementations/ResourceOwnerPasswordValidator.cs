using Common.Domain.Multi;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Membership.Application.Model;
using Membership.Application.Queries;
using Membership.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.Protocol.Implementations
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserQueries userQueries;
        private readonly CredentialsService credentialsService;

        public ResourceOwnerPasswordValidator(UserQueries userQueries, CredentialsService credentialsService)
        {
            this.userQueries = userQueries;
            this.credentialsService = credentialsService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var username = new Username(context.UserName);
            var isValid = await credentialsService.VerifyCredentials(username, context.Password);

            if (isValid)
            {
                var user = await userQueries.FindUser(username);
                ValidGrantValidationResult(user, context);

                return;
            }

            var email = new Email(context.UserName);
            isValid = await credentialsService.VerifyCredentials(email, context.Password);

            if (isValid)
            {
                var user = await userQueries.FindUser(email);
                ValidGrantValidationResult(user, context);
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Username or password does not match", null);
        }

        private void ValidGrantValidationResult(UserModel user, ResourceOwnerPasswordValidationContext context)
        {
            var claims = new List<System.Security.Claims.Claim>();

            user.Claims.ForEach(claim => claims.Add(new System.Security.Claims.Claim(claim.Key, claim.Value)));
            context.Result = new GrantValidationResult(user.Id, "password", claims, "local", null);
        }
    }
}
