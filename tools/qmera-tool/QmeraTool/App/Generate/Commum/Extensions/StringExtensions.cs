namespace QmeraTool.App.Generate.Commum.Extensions;

public static class StringExtensions
{
    public static string ToPascalCase(string str)
    {
        var words = str.Split(["_"], StringSplitOptions.RemoveEmptyEntries);

        var values = words
          .Select(w =>
              string.Concat(w.First().ToString().ToUpper(),
                w.AsSpan(1)));

        var pascalCase = string.Join("", values);

        return pascalCase;
    }
}