using IdentityModel.Client;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProxyKit;
using System;
using Web.Common;

namespace Web.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProxy();
            services.AddAccessTokenManagement();
            services.AddControllers().AddApplicationPart(typeof(Endpoints).Assembly);            
            services.AddSpaStaticFiles(options =>
            {
                options.RootPath = "Resources/dist";
            });
            services.AddResponseCompression();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("cookies", options =>
            {
                // App Session Id
                options.Cookie.Name = Env.GetString("APP_SESSION_COOKIE_NAME");
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.SlidingExpiration = true;
                options.SessionStore = new TicketStore(new RedisCacheOptions()
                {
                    Configuration = Env.GetString("REDIS_CONNECTION")
                }, Env.GetString("APP_COOKIE_KEY_PREFIX"));
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = Env.GetString("AUTH_HTTPS_URL");
                options.ClientId = Env.GetString("APP_AUTH_CLIENT_ID");
                options.ClientSecret = Env.GetString("APP_AUTH_CLIENT_SECRET");

                options.ResponseType = "code";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("api");
                options.Scope.Add("offline_access");
            });

            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = Env.GetString("APP_CSRF_COOKIE_NAME");
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.HeaderName = "X-XSRF-TOKEN";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<StrictSameSiteMiddleware>();
            app.UseAuthentication();
            app.UseResponseCompression();

            app.Use(async (context, next) =>
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    await context.ChallengeAsync();
                    return;
                }

                await next();
            });

            app.Use(async (context, next) =>
            {
                if (HttpMethods.IsPost(context.Request.Method) ||
                    HttpMethods.IsPatch(context.Request.Method) ||
                    HttpMethods.IsPut(context.Request.Method) ||
                    HttpMethods.IsDelete(context.Request.Method))
                {
                    await antiforgery.ValidateRequestAsync(context);
                }

                await next();
            });

            app.Use(next => context =>
            {
                string path = context.Request.Path.Value;

                if (
                    string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
                {
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Headers.Append("X-XSRF-TOKEN", tokens.RequestToken);
                }

                return next(context);
            });

            app.Map("/api", api =>
            {
                api.RunProxy(async context =>
                {
                    var forwardContext = context.ForwardTo(Env.GetString("API_HTTPS_URL"));

                    try
                    {
                        var token = await context.GetUserAccessTokenAsync();
                        forwardContext.UpstreamRequest.SetBearerToken(token);

                        return await forwardContext.Send();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
            });

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Resources";

                if (env.IsDevelopment())
                {
                    spa.UseVueDevelopmentServer(Env.GetString("APP_VUE_DEVELOPMENT_SERVER_URL"));
                }
            });
        }
    }
}
