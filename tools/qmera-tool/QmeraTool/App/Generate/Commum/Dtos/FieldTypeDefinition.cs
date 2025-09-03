namespace QmeraTool.App.Generate.Commum.Dtos;

public record FieldTypeDefinition
(
    string FieldName,
    string TypeName,
    bool IsNullable,
    int MaxLength
)
{
    private static readonly List<string> ValidDataTypes = [
        "string",
        "int",
        "bool",
        "decimal",
        "DateTime",
        "Guid",
        "byte",
        "sbyte",
        "short",
        "ushort",
        "long",
        "ulong",
        "float",
        "double",
        "char",
        "object",
    ];

    public static FieldTypeDefinition FromRawString(string raw)
    {
        var tokens = raw.Split(":");

        if (tokens.Length < 2)
        {
            throw new ArgumentException("Invalid raw string");
        }

        var fieldName = tokens[0];
        var typeName = tokens[1];
        var isNullable = false;
        var maxLenghtParam = tokens.ElementAtOrDefault(2);

        if (typeName.EndsWith('?'))
        {
            typeName = typeName.Remove(typeName.Length - 1);
            isNullable = true;
        }

        if (!ValidDataTypes.Contains(typeName))
        {
            throw new ArgumentException($"Invalid type name: {typeName}");
        }

        var maxLength = 0;

        if (maxLenghtParam is not null)
        {
            if (!int.TryParse(maxLenghtParam, out var maxLengthParsed))
            {
                throw new ArgumentException("Invalid max length, should be a number");
            }

            maxLength = maxLengthParsed;
        }

        return new(
            fieldName,
            typeName,
            isNullable,
            maxLength
        );
    }
}