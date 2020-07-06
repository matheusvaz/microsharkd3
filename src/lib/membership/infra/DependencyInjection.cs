using MediatR;
using Membership.Application.Commands.CreateUser;
using Membership.Application.Queries;
using Membership.Domain;
using Membership.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Infra
{
    public static class DependencyInjection
    {
        public static void AddMembershipDependencies(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Commands
            services.AddScoped<IRequestHandler<CreateUserCommand, bool>, CreateUserCommandHandler>();

            // Services
            services.AddScoped<CredentialsService>();

            // Queries
            services.AddScoped<UserQueries>();
        }
    }
}
