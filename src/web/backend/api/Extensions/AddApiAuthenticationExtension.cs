using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.Extensions
{
    public static class AddApiAuthenticationExtension
    {
        public static void AddApiAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Env.GetString("AUTH_HTTPS_URL");
                    options.ApiName = "api";
                    options.ApiSecret = Env.GetString("API_SECRET");
                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10);
                });
        }
    }
}
