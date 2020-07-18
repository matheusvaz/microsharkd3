using Auth.Protocol.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Web.Auth;
using Web.Auth.Protocol;
using Web.Auth.Protocol.Resources;

namespace auth.extensions
{
    public static class AddIdentityServer
    {
        public static void AddIdentityServer4(this IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer(options =>
            {
                // OAuth Check Session Id Cookie
                options.Authentication.CheckSessionCookieName = "oachksid";
            })
            .AddInMemoryClients(Clients.All)
            .AddInMemoryIdentityResources(Identity.All)
            .AddInMemoryApiResources(Apis.All)
            .AddInMemoryApiScopes(Apis.Scopes)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseMySql(
                    Env.GetString("AUTH_DB_CONNECTION"),
                    sql => sql.MigrationsAssembly(migrationsAssembly)
                );
            })
            .AddProfileService<ProfileService>()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddDeveloperSigningCredential();

            services.AddAntiforgery(options =>
            {
                // OAuth Cross-Site Request Forgery
                options.Cookie.Name = "oacsrf";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    // OAuth Session Id
                    options.Cookie.Name = "oasid";
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
        }
    }
}
