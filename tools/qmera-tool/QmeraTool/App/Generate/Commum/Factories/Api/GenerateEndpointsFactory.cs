using System.Text;

using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Extensions;

namespace QmeraTool.App.Generate.Commum.Factories.Api;

public record GenerateEndpointsFactoryInput(ScaffoldInput Input, ProjectMetadata Metadata);

public record GenerateEndpointsFactoryOutput(string Content, string FileNamespace, string MapMethodName);

public static class GenerateEndpointsFactory
{
    public static GenerateEndpointsFactoryOutput Generate(GenerateEndpointsFactoryInput command)
    {
        var (input, metadata) = command;

        var sb = new StringBuilder();
        var entityName = StringExtensions.ToPascalCase(input.Entity);
        var entityNameKebabCase = StringExtensions.ToKebabCase(input.Entity);

        var methodName = $"Map{entityName}Endpoints";
        var fileNamespace = $"{metadata.SolutionName}.Modules.{input.Module}.Api.Endpoints";

        var fileContent =
$@"using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using {metadata.SolutionName}.Modules.Commum.Adapters.Databases;
using {metadata.SolutionName}.Modules.{input.Module}.Domain.Entities;

namespace {fileNamespace};

public static class {entityName}Endpoint
{{
    public static void {methodName}(this WebApplication app)
    {{

        var g = app.MapGroup(""{entityNameKebabCase}"")
                .WithTags(""{entityNameKebabCase}"")
        ;

        g.MapGet("""", async (
                    [FromServices] ApplicationDbContext db) =>
        {{
            var items = await db.{entityName}.ToListAsync();

            return items;
        }});

        g.MapGet(""{{id}}"", async (
                    [FromServices] ApplicationDbContext db,
                    [FromRoute] int id) =>
        {{
            var item = await db.{entityName}.FindAsync(id);

            return item;
        }});

        g.MapPost("""", async (
                    [FromServices] ApplicationDbContext db,
                    [FromBody] {entityName} item) =>
        {{
            db.{entityName}.Add(item);

            await db.SaveChangesAsync();

            return item;
        }});

        g.MapPut(""{{id}}"", async (
                    [FromServices] ApplicationDbContext db,
                    [FromRoute] int id,
                    [FromBody] {entityName} item
                ) =>
        {{
            var itemToUpdate = await db.{entityName}.FindAsync(id);

            if (itemToUpdate is null)
            {{
                return Results.NotFound();
            }}

            db.{entityName}.Update(item);

            await db.SaveChangesAsync();

            return Results.Ok(item);
        }});

        g.MapDelete(""{{id}}"", async (
                    [FromServices] ApplicationDbContext db,
                    [FromRoute] int id) =>
        {{
            var item = await db.{entityName}.FindAsync(id);

            if (item is null)
            {{
                return Results.NotFound();
            }}

            db.{entityName}.Remove(item);

            await db.SaveChangesAsync();

            return Results.NoContent();
        }});
    }}
}}
";

        sb.AppendLine(fileContent);

        var content = sb.ToString();

        return new(
            Content: content,
            FileNamespace: fileNamespace,
            MapMethodName: methodName);
    }
}