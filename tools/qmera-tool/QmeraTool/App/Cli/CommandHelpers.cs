using QmeraTool.App.Commum;

using Spectre.Console;

namespace QmeraTool.App.Cli;

public static class CommandHelpers
{
    public static string? GetProjectPath(string? path = null, bool ignoreTestProjects = false)
    {
        var files = ProjectScanner.SearchForCsProjFiles(path)
            ?.Where(x => !ignoreTestProjects || !x.Contains("test", StringComparison.CurrentCultureIgnoreCase))
            .ToArray()
            ;

        if (files is null || files.Length is 0)
        {
            return null;
        }

        if (files.Length is 1)
        {
            return files[0];
        }

        var prompt = new SelectionPrompt<string>()
              .Title("What's your [green]favorite .csproj file[/]?")
              .EnableSearch()
              .PageSize(5)
              .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
              .AddChoices(files);

        return AnsiConsole.Prompt(prompt);
    }
}