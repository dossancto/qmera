using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Extensions;
using QmeraTool.App.Generate.Commum.Factories.Api;

namespace QmeraTool.App.Generate.Commum.Usecases;

public static class CreateApiResourceUsecase
{
    public static void Create(ScaffoldInput input, ProjectMetadata metadata)
    {
        var pathToModule = Path.Combine(metadata.ProjectPath, "Modules", input.Module);

        // Generate my entities files 
        GenerateSchemeUsecase.ExecuteWithoutModule(input, metadata);

        var apiContent = GenerateEndpointsFactory.Generate(new(input, metadata));

        var formatedName = StringExtensions.ToPascalCase(input.Entity);

        var pathToEndpoints = Path.Combine(pathToModule, "Api", "Endpoints");
        var endpointFile = Path.Combine(pathToEndpoints, $"{formatedName}Endpoint.cs");

        Directory.CreateDirectory(pathToEndpoints);

        File.WriteAllText(endpointFile, apiContent.Content);

        var moduleAlreadyExists = GenerateModuleUsecase.TryCreateApiModule(input, metadata, pathToModule, apiContent.MapMethodName);

    }
}