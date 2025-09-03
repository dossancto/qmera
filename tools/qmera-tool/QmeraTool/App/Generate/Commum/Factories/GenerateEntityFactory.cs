using System.Text;

using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Extensions;

namespace QmeraTool.App.Generate.Commum.Factories;

public static class GenerateEntityFactory
{
    public static string Generate(ScaffoldInput input, ProjectMetadata metadata)
    {
        var fieldsDtos = input.Fields
          .Select(FieldTypeDefinition.FromRawString)
          .ToList();

        var sb = new StringBuilder();

        sb.AppendLine($"namespace {metadata.SolutionName}.Modules.{input.Module}.Domain.Entities;");
        sb.AppendLine();
        sb.AppendLine($"public class {StringExtensions.ToPascalCase(input.Entity)}");
        sb.AppendLine("{");

        foreach (var fieldDto in fieldsDtos)
        {
            var fieldNameAsPascalCase = StringExtensions.ToPascalCase(fieldDto.FieldName);
            var initValue = GetDefaultInitValue(fieldDto);

            var formatedInitValue = initValue is null ? "" : $"= {initValue};";
            var nullable = fieldDto.IsNullable ? "?" : "";

            sb.AppendLine();
            sb.AppendLine($"    public {fieldDto.TypeName}{nullable} {fieldNameAsPascalCase} {{ get; set; }} {formatedInitValue}");
        }

        sb.AppendLine("}");

        var content = sb.ToString();

        return content;
    }

    private static string? GetDefaultInitValue(FieldTypeDefinition field)
    {
        var typeName = field.TypeName;

        if (field.IsNullable)
        {
            return null;
        }

        if (typeName.StartsWith("List"))
        {
            return "[]";
        }

        if (typeName is "string")
        {
            return "string.Empty";
        }

        return null;
    }

}