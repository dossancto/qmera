using System.Diagnostics;

using Cocona;

using QmeraTool.App.Cli;
using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Usecases;

using Spectre.Console;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand(
        "app.server",
        async (
                [Option(Description = "The path to the project")] string? project,
                [Option(Description = "Option to disable output terminal colors")] bool disableColors = false
                ) =>
        {
            var choosedFile = CommandHelpers.GetProjectPath(project, true);

            if (choosedFile is null)
            {
                AnsiConsole.MarkupLine($"[red]No .csproj file found in the current directory {project}[/]");
                return;
            }

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"run --project \"{choosedFile}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            processStartInfo.EnvironmentVariables["DOTNET_SYSTEM_CONSOLE_ALLOW_ANSI_COLOR_REDIRECTION"] = (!disableColors).ToString();

            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.OutputDataReceived += (sender, e)
                    => Console.WriteLine(e.Data);

                process.ErrorDataReceived += (sender, e)
                    => Console.Error.WriteLine(e.Data);

                process.Start();

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Wait for the process to exit.
                await process.WaitForExitAsync();
            }
        }
    )
    .WithDescription("Run Qmera Application");
;

app.AddCommand(
    "gen.scheme",
    (
     [Argument(Description = "The App module")] string module,
     [Argument(Description = "The entity to be used")] string entity,
     [Argument(Description = "Fields")] List<string> fields,
     [Option(Description = "The path to the project")] string? path
    ) =>
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
    })
    .WithDescription("Gen Application Scheme only");

app.Run();