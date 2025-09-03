namespace QmeraTool.App.Generate.Commum.Dtos;

public record ProjectMetadata
(
    string Name,
    string SolutionName,
    string ProjectPath
)
{
    public static ProjectMetadata FromCsProjPath(string path)
    {
        var name = Path.GetFileNameWithoutExtension(path);
        var solutionName = Path.GetFileNameWithoutExtension(path);

        return new ProjectMetadata(
            Name: name,
            SolutionName: solutionName,
            ProjectPath: path);
    }
}