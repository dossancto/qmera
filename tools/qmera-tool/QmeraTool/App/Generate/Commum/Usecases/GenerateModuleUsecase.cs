using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Extensions;
using QmeraTool.App.Generate.Commum.Factories;

namespace QmeraTool.App.Generate.Commum.Usecases;

public record GenerateModuleUsecaseOutput
(
    bool ModuleAlreadyExists
);

public static class GenerateModuleUsecase
{
    public static GenerateModuleUsecaseOutput TryCreateRawModule(ScaffoldInput input, ProjectMetadata metadata, string pathToModule)
        => TryCreateModule(input, metadata, pathToModule, GenerateModuleFactory.Generate(input, metadata));


    public static GenerateModuleUsecaseOutput TryCreateApiModule(
        ScaffoldInput input,
        ProjectMetadata metadata,
        string pathToModule,
        string apiMappingMethodName)
        => TryCreateModule(input, metadata, pathToModule, GenerateModuleFactory.GenerateApiMapping(input, metadata, apiMappingMethodName));

    private static GenerateModuleUsecaseOutput TryCreateModule(
            ScaffoldInput input,
            ProjectMetadata metadata,
            string pathToModule,
            string moduleContent
            )
    {
        var formatedModuleName = StringExtensions.ToPascalCase(input.Module);

        var pathToModuleFile = Path.Combine(pathToModule, $"{formatedModuleName}Module" + ".cs");

        var moduleAlreadyExists = Path.Exists(pathToModuleFile);

        if (moduleAlreadyExists)
        {
            return new(true);
        }

        Directory.CreateDirectory(pathToModule);

        File.WriteAllText(pathToModuleFile, moduleContent);

        return new(false);
    }
}