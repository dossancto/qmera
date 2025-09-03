namespace QmeraTool.App.Generate.Commum.Dtos;

public record ScaffoldInput
(
    string Module,
    string Entity,
    List<string> Fields
)
{
}