using System.Diagnostics;

using Cocona;

using QmeraTool.App.Cli;
using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Usecases;

using Spectre.Console;

namespace QmeraTool.App.Commands;

public static class GenerateCommands
{
    public static async Task GenerateScheme(
        [Argument(Description = "The App module")] string module,
        [Argument(Description = "The entity to be used")] string entity,
        [Argument(Description = "Fields")] List<string> fields,
        [Option(Description = "The path to the project")] string? path
    )
    {
        var choosedFile = CommandHelpers.GetProjectPath(path, true);

        if (choosedFile is null)
        {
            AnsiConsole.MarkupLine("[red]No .csproj file found in the current directory[/]");
            return;
        }

        var projectMetadata = ProjectMetadata.FromCsProjPath(choosedFile);
        var scaffoldInput = new ScaffoldInput(
            Module: module,
            Entity: entity,
            Fields: fields
        );

        GenerateSchemeUsecase.Execute(scaffoldInput, projectMetadata);
    }

    public static async Task GenerateApi(
        [Argument(Description = "The App module")] string module,
        [Argument(Description = "The entity to be used")] string entity,
        [Argument(Description = "Fields")] List<string> fields,
        [Option(Description = "The path to the project")] string? path
    )
    {
        var choosedFile = CommandHelpers.GetProjectPath(path, true);

        if (choosedFile is null)
        {
            AnsiConsole.MarkupLine("[red]No .csproj file found in the current directory[/]");
            return;
        }

        var projectMetadata = ProjectMetadata.FromCsProjPath(choosedFile);
        var scaffoldInput = new ScaffoldInput(
            Module: module,
            Entity: entity,
            Fields: fields
        );

        CreateApiResourceUsecase.Create(scaffoldInput, projectMetadata);

    }


    public static async Task GenerateWeb(
        [Argument(Description = "The App module")] string module,
        [Argument(Description = "The entity to be used")] string entity,
        [Argument(Description = "Fields")] List<string> fields,
        [Option(Description = "The path to the project")] string? path
    )
    {
        var choosedFile = CommandHelpers.GetProjectPath(path, true);

        if (choosedFile is null)
        {
            AnsiConsole.MarkupLine("[red]No .csproj file found in the current directory[/]");
            return;
        }

        var projectMetadata = ProjectMetadata.FromCsProjPath(choosedFile);
        var scaffoldInput = new ScaffoldInput(
            Module: module,
            Entity: entity,
            Fields: fields
        );

        CreateWebPageResourceUsecase.Create(scaffoldInput, projectMetadata);

    }
}