using QmeraTool.App.Generate.Commum.Dtos;

namespace QmeraTool.Tests.Modules.Generate.Commum;

public class FieldTypeDefinitionTest
{
    [Fact]
    public void FieldTypeDefinitionTest_ShoudParseFromRawString()
    {
        //Given
        var raw = "name:string";

        //When
        var field = FieldTypeDefinition.FromRawString(raw);

        //Then
        Assert.Equal("name", field.FieldName);
        Assert.Equal("string", field.TypeName);
        Assert.False(field.IsNullable);
        Assert.Equal(0, field.MaxLength);
    }

    [Fact]
    public void FieldTypeDefinitionTest_ShoudParseFromRawString_WhenNullable()
    {
        //Given
        var raw = "name:string?";

        //When
        var field = FieldTypeDefinition.FromRawString(raw);

        //Then
        Assert.Equal("name", field.FieldName);
        Assert.Equal("string", field.TypeName);
        Assert.True(field.IsNullable);
        Assert.Equal(0, field.MaxLength);
    }

    [Fact]
    public void FieldTypeDefinitionTest_ShoudParseFromRawString_WhenNullableAndMaxLenght()
    {
        //Given
        var raw = "name:string?:64";

        //When
        var field = FieldTypeDefinition.FromRawString(raw);

        //Then
        Assert.Equal("name", field.FieldName);
        Assert.Equal("string", field.TypeName);
        Assert.True(field.IsNullable);
        Assert.Equal(64, field.MaxLength);
    }

    [Fact]
    public void FieldTypeDefinitionTest_ShoudParseFromRawString_WhenMaxLenght()
    {
        //Given
        var raw = "name:string:64";

        //When
        var field = FieldTypeDefinition.FromRawString(raw);

        //Then
        Assert.Equal("name", field.FieldName);
        Assert.Equal("string", field.TypeName);
        Assert.False(field.IsNullable);
        Assert.Equal(64, field.MaxLength);
    }

    [Fact]
    public void FieldTypeDefinitionTest_ReturnError_WhenNotAllTokens()
    {
        //Given
        var raw = "name";

        //When
        var field = () => FieldTypeDefinition.FromRawString(raw);

        //Then
        Assert.Throws<ArgumentException>(field);
    }

    [Fact]
    public void FieldTypeDefinitionTest_ReturnError_WhenMaxLenghtIsNotANumber()
    {
        //Given
        var raw = "name:string?:sometext";

        //When
        var field = () => FieldTypeDefinition.FromRawString(raw);

        //Then
        Assert.Throws<ArgumentException>(field);
    }
}