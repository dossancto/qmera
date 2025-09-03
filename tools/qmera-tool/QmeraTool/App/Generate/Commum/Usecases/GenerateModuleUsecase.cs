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
        => TryCreateModule(input, metadata, pathToModule, GenerateModuleFactory.Generate);


    public static GenerateModuleUsecaseOutput TryCreateApiModule(ScaffoldInput input, ProjectMetadata metadata, string pathToModule)
        => TryCreateModule(input, metadata, pathToModule, GenerateModuleFactory.Generate);

    private static GenerateModuleUsecaseOutput TryCreateModule(
            ScaffoldInput input,
            ProjectMetadata metadata,
            string pathToModule,
            Func<ScaffoldInput,
            ProjectMetadata,
            string> generateModule)
    {
        var formatedModuleName = StringExtensions.ToPascalCase(input.Module);

        var pathToModuleFile = Path.Combine(pathToModule, $"{formatedModuleName}Module" + ".cs");

        var moduleAlreadyExists = Path.Exists(pathToModuleFile);

        if (moduleAlreadyExists)
        {
            return new(true);
        }

        Directory.CreateDirectory(pathToModule);

        var moduleContent = generateModule(input, metadata);

        File.WriteAllText(pathToModuleFile, moduleContent);

        return new(false);
    }
}