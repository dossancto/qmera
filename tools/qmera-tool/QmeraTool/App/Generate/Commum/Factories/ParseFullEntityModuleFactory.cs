using QmeraTool.App.Generate.Commum.Dtos;

namespace QmeraTool.App.Generate.Commum.Factories;

public static class ParseFullEntityModuleFactory
{
    public static void Create(ScaffoldInput input)
    {
        var fieldsDtos = input.Fields
          .Select(FieldTypeDefinition.FromRawString)
          .ToList();

    }
}