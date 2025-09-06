using System.Text;

using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Extensions;

namespace QmeraTool.App.Generate.Commum.Factories;

public static class GenerateModuleFactory
{
    public static string Generate(ScaffoldInput input, ProjectMetadata metadata)
    {
        var sb = new StringBuilder();

        var formatedModuleName = StringExtensions.ToPascalCase(input.Module);

        sb.AppendLine($"namespace {metadata.SolutionName}.Modules.{formatedModuleName};");
        sb.AppendLine();
        sb.AppendLine($"public static class {formatedModuleName}Module");
        sb.AppendLine("{");

        sb.AppendLine($"    public static IServiceCollection Add{formatedModuleName}Module(this IServiceCollection services)");

        sb.AppendLine("    {");
        sb.AppendLine("        return services;");
        sb.AppendLine("    }");

        sb.AppendLine();

        sb.AppendLine($"    public static WebApplication Use{formatedModuleName}Module(this WebApplication app)");

        sb.AppendLine("    {");
        sb.AppendLine("        return app;");
        sb.AppendLine("    }");

        sb.AppendLine("}");

        var content = sb.ToString();

        return content;
    }

    public static string GenerateApiMapping(ScaffoldInput input, ProjectMetadata metadata, string apiMappingMethodName)
    {
        var sb = new StringBuilder();

        var formatedModuleName = StringExtensions.ToPascalCase(input.Module);

        sb.AppendLine($"using {metadata.SolutionName}.Modules.{input.Module}.Api.Endpoints;");
        sb.AppendLine($"namespace {metadata.SolutionName}.Modules.{formatedModuleName};");
        sb.AppendLine();
        sb.AppendLine($"public static class {formatedModuleName}Module");
        sb.AppendLine("{");

        sb.AppendLine($"    public static IServiceCollection Add{formatedModuleName}Module(this IServiceCollection services)");

        sb.AppendLine("    {");
        sb.AppendLine("        return services;");
        sb.AppendLine("    }");

        sb.AppendLine();

        sb.AppendLine($"    public static WebApplication Use{formatedModuleName}Module(this WebApplication app)");

        sb.AppendLine("    {");
        sb.AppendLine($"        app.{apiMappingMethodName}();");
        sb.AppendLine("        return app;");
        sb.AppendLine("    }");

        sb.AppendLine("}");

        var content = sb.ToString();

        return content;
    }
}