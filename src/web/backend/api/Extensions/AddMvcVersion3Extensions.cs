using Api.Filters;
using Common.Domain.Multi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Api.Extensions
{
    public static class AddMvcVersion3Extensions
    {
        public static void AddMvcVersion3(this IServiceCollection services)
        {
            services.AddApiVersioning(conf =>
            {
                conf.ReportApiVersions = true;
                conf.AssumeDefaultVersionWhenUnspecified = false;
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddMvc(options =>
            {
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => Translation.Key("MVC.ATTEMPTED_VALUE_IS_INVALID_ACCESSOR"));
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => Translation.Key("MVC.MISSING_BIND_REQUIRED_VALUE_ACCESSOR"));
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => Translation.Key("MVC.MISSING_KEY_OR_VALUE_ACCESSOR"));
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => Translation.Key("MVC.MISSING_REQUEST_BODY_REQUIRED_VALUE_ACCESSOR"));
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => Translation.Key("MVC.NON_PROPERTY_ATTEMPTED_VALUE_IS_INVALID_ACCESSOR"));
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => Translation.Key("MVC.NON_PROPERTY_UNKNOWN_VALUE_IS_INVALID_ACCESSOR"));
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => Translation.Key("MVC.NON_PROPERTY_VALUE_MUST_BE_A_NUMBER_ACCESSOR"));
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => Translation.Key("MVC.UNKNOWN_VALUE_IS_INVALID_ACCESSOR"));
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => Translation.Key("MVC.VALUE_IS_INVALID_ACCESSOR"));
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => Translation.Key("MVC.VALUE_MUST_BE_A_NUMBER_ACCESSOR"));
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => Translation.Key("MVC.VALUE_MUST_NOT_BE_NULL_ACCESSOR"));

                options.Filters.Add(new NotificationErrorFilter());

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var modelState = new ValidationProblemDetails(context.ModelState);
                    var response = new
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        Title = Translation.Key("VALIDATION.RESPONSE_TITLE"),
                        Status = 400,
                        TraceId = context.HttpContext.TraceIdentifier,
                        Errors = modelState.Errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            services.AddControllers();
            services.AddResponseCompression();
            services.AddHttpContextAccessor();
        }
    }
}
