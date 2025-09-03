using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Factories;

namespace QmeraTool.Tests.Modules.Generate.Commum.Factories;

public class GenerateModuleFactoryTest
{
    [Fact]
    public void GenerateModuleFactoryTest_()
    {
        //Given
        var input = new ScaffoldInput(
            Module: "Products",
            Entity: "Product",
            Fields: [
              "name:string",
              "age:int",
              "created_at:DateTime"
            ]
        );

        var metadata = new ProjectMetadata(
            "MyEcommerce",
            "MyEcommerce",
            "/tmp"
          );

        //When
        var res = GenerateModuleFactory.Generate(input, metadata);

        File.WriteAllText("/tmp/Module.cs", res);

        //Then

    }
}