using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class AddRedisCacheExtension
    {
        public static void AddRedisCache(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "API_";
            });
        }
    }
}
