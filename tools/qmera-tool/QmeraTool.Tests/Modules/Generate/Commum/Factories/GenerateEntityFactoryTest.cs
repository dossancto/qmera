using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Factories;

namespace QmeraTool.Tests.Modules.Generate.Commum.Factories;

public class GenerateEntityFactoryTest
{
    [Fact]
    public void GenerateEntityFactoryTest_ShouldGenerateEntityFile()
    {
        //Given
        var input = new ScaffoldInput("Module",
            "entity",
            [
              "name:string",
          "age:int",
          "created_at:DateTime"
            ]
            )
        ;

        var metadata = new ProjectMetadata(
            "Module",
            "Module",
            "/tmp"
          );

        //When
        var res = GenerateEntityFactory.Generate(input, metadata);

        //Then
        Assert.NotNull(res);
        Assert.Contains("namespace Module.Modules.Module.Domain.Entities;", res);
        Assert.Contains("public class Entity", res);
        Assert.Contains("public string Name { get; set; } = string.Empty;", res);
        Assert.Contains("public int Age { get; set; }", res);
        Assert.DoesNotContain("public int Age { get; set; } =", res);
        Assert.Contains("public DateTime CreatedAt { get; set; }", res);
    }

    [Fact]
    public void GenerateEntityFactoryTest_ShouldGenerateEntityFile_PrimitiveData()
    {
        //Given
        var input = new ScaffoldInput(
            "Module",
            "Entity",
            [
              "age:int"
            ]
            )
        ;

        var metadata = new ProjectMetadata(
            "Module",
            "Module",
            "/tmp"
          );

        //When
        var res = GenerateEntityFactory.Generate(input, metadata);

        //Then
        Assert.NotNull(res);
        Assert.Contains("public int Age { get; set; }", res);
        Assert.DoesNotContain("public int Age { get; set; } =", res);
    }

    [Fact]
    public void GenerateEntityFactoryTest_ShouldGenerateEntityFile_NullableFields()
    {
        //Given
        var input = new ScaffoldInput(
            "Module",
            "Entity",
            [
              "description:string?"
            ]
            )
        ;

        var metadata = new ProjectMetadata(
            "Module",
            "Module",
            "/tmp"
          );

        //When
        var res = GenerateEntityFactory.Generate(input, metadata);

        //Then
        Assert.NotNull(res);
        Assert.Contains("public string? Description { get; set; }", res);
        Assert.DoesNotContain("public string? Description { get; set; } =", res);
    }
}