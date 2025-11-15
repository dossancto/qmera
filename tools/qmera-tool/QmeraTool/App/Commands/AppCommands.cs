using System.Diagnostics;

using Cocona;

using QmeraTool.App.Cli;

using Spectre.Console;

namespace QmeraTool.App.Commands;

public class AppCommands
{
    public static async Task AppServer(
                    [Option(Description = "The path to the project")] string? project,
                    [Option(Description = "Option to disable output terminal colors")] bool disableColors = false

                )
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
}