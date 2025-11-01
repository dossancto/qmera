using Microsoft.AspNetCore.OpenApi;

namespace QmeraApi.Modules.Commum.Configuration.API.Configurations.ValidatorOnOpenAPI;

/// <summary>
/// Provides extension methods for integrating Obsolete attribute handling with Microsoft.AspNetCore.OpenApi.
/// </summary>
public static class OpenApiExtensions
{
    /// <summary>
    /// Adds schema transformers that incorporate Obsolete attributes into the OpenAPI document.
    /// </summary>
    /// <param name="options">The <see cref="OpenApiOptions"/> to configure.</param>
    /// <returns>The configured <see cref="OpenApiOptions"/>.</returns>
    public static OpenApiOptions AddObsoluteAttributes(this OpenApiOptions options)
    {
        // Register the Obsolete attribute schema transformer
        options.AddSchemaTransformer<ObsoleteSchemaTransformer>();
        return options;
    }
}