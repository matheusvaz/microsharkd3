using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace Web.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(
                        Environment.GetEnvironmentVariable("APP_HTTP_URL"),
                        Environment.GetEnvironmentVariable("APP_HTTPS_URL")
                    );
                });
    }
}
