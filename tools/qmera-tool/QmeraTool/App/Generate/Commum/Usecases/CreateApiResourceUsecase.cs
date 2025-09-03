using QmeraTool.App.Generate.Commum.Dtos;

namespace QmeraTool.App.Generate.Commum.Usecases;

public static class CreateApiResourceUsecase
{
    public static void Create(ScaffoldInput input, ProjectMetadata metadata)
    {
        var pathToModule = Path.Combine(metadata.ProjectPath, "Modules", input.Module);

        // Generate my entities files 
        GenerateSchemeUsecase.ExecuteWithoutModule(input, metadata);

        // Generate the API module
        var moduleAlreadyExists = GenerateModuleUsecase.TryCreateApiModule(input, metadata, pathToModule);
    }
}