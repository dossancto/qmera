namespace QmeraTool.App.Commum;

public static class ProjectScanner
{
    public static string[] SearchForCsProjFiles(string? path = null)
    {
        path ??= Directory.GetCurrentDirectory();

        var slnFile = Directory.GetFiles(path, "*.sln", SearchOption.TopDirectoryOnly)
            .FirstOrDefault()
            ?.Replace(".sln", "")
            ;

        var slnFileName = Path.GetFileName(slnFile);

        var webAssemblycsprojName = $"{slnFileName}.Client.csproj";

        var files = Directory.GetFiles(
            path: path,
            searchPattern: "*.csproj",
            searchOption: SearchOption.AllDirectories)
            .Where(file => !file.Contains(webAssemblycsprojName))
            .ToArray()
            ;

        if (files is null or { Length: 0 })
        {
            return [];
        }

        var filePathWithoutFileName = files
            .Select(Path.GetDirectoryName)
            .Where(x => x is not null)
            .Cast<string>()
            .Distinct()
            .ToArray()
            ;

        return filePathWithoutFileName;
    }
}