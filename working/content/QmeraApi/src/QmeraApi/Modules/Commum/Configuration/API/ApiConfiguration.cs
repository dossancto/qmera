using System.Text.Json.Serialization;

using FluentValidation;

using MicroElements.NSwag.FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

using QmeraApi.Modules.Commum.Adapters.UI.Middlewares;
using QmeraApi.Modules.Commum.Configuration.API.Configurations;

using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace QmeraApi.Modules.Commum.Configuration.API;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options
                     => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.Configure<ApiBehaviorOptions>(options
                => options.SuppressModelStateInvalidFilter = true);

        services.AddExceptionHandler<ErrorHandlerMiddleware>();

        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;

                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });

        FluentValidationConfiguration(services);

        services.AddEndpointsApiExplorer();

        return services;
    }

    private static void FluentValidationConfiguration(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>(
            lifetime: ServiceLifetime.Singleton);

        services.AddFluentValidationAutoValidation(o =>
        {
            o.OverrideDefaultResultFactoryWith<ProblemDetailsResultFactory>();
        });

        services.AddFluentValidationRulesToSwagger();
        ;
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        app.UseExceptionHandler(new ExceptionHandlerOptions()
        {
            AllowStatusCode404Response = true,
        });
    }
}