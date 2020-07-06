using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Infra.Extension
{
    public static class TranslationExtension
    {
        public static void AddTranslation(this IServiceCollection services)
        {
            var defaultCulture = "en-US";

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(defaultCulture);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(defaultCulture);

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Path.Combine(projectPath, @"src\Core\Common\Mindbills.Core.Common.Domain\Resources");

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(conf =>
            {
                CultureInfo[] supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pt-BR")
                };

                conf.DefaultRequestCulture = new RequestCulture(new CultureInfo(defaultCulture));
                conf.SupportedCultures = supportedCultures;
                conf.SupportedUICultures = supportedCultures;
                conf.RequestCultureProviders.Clear();
                conf.RequestCultureProviders.Insert(
                    0,
                    new CustomRequestCultureProvider(
                        httpContext =>
                        {
                            if (httpContext == null)
                            {
                                throw new ArgumentNullException(nameof(httpContext));
                            }

                            var path = httpContext.Request.Path;
                            var regex = new Regex(@"^\/v[0-9]\/([\w]{2}-[\w]{2}\/)");
                            var match = regex.Match(path);

                            if (match.Success)
                            {
                                var culture = match.Groups[1].ToString().TrimEnd('/');
                                return Task.FromResult(new ProviderCultureResult(culture));
                            }

                            return Task.FromResult(new ProviderCultureResult("en-US"));
                        }
                    )
                );
            });
        }
    }
}
