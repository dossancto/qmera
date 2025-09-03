using QmeraTool.App.Generate.Commum.Dtos;
using QmeraTool.App.Generate.Commum.Factories;

namespace QmeraTool.Tests.Modules.Generate.Commum.Factories;

public class EntityConfigurationFactoryTest
{
    [Fact]
    public void EntityConfigurationFactoryTest_()
    {
        //Given
        var input = new ScaffoldInput("Module",
            "entity",
            [
              "id:Guid",
              "name:string",
              "description:string?:32",
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

        var res = EntityConfigurationFactory.Create(input, metadata);

        //Then

        Assert.Contains("""
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Modules.Module.Domain.Entities;
namespace Module.Modules.Module.Adapters.Database.Configurations;

public class EntityEntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .IsRequired()
            ;

        builder.Property(t => t.Name)
            .IsRequired()
            ;

        builder.Property(t => t.Description)
            .HasMaxLength(32)
            ;

        builder.Property(t => t.Age)
            .IsRequired()
            ;

        builder.Property(t => t.CreatedAt)
            .IsRequired()
            ;

}
}
""", res);
    }

    [Fact]
    public void EntityConfigurationFactoryTest_ShouldApplyMaxLenghtOnlyToString()
    {
        //Given
        var input = new ScaffoldInput("Module",
            "entity",
            [
              "name:string",
              "description:string?:32",
              "age:int",
              "created_at:DateTime:32"
            ]
        )
        ;

        var metadata = new ProjectMetadata(
            "Module",
            "Module",
            "/tmp"
          );

        //When

        var res = EntityConfigurationFactory.Create(input, metadata);

        //Then

        Assert.Contains("""
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Modules.Module.Domain.Entities;
namespace Module.Modules.Module.Adapters.Database.Configurations;

public class EntityEntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired()
            ;

        builder.Property(t => t.Description)
            .HasMaxLength(32)
            ;

        builder.Property(t => t.Age)
            .IsRequired()
            ;

        builder.Property(t => t.CreatedAt)
            .IsRequired()
            ;

}
}
""", res);

    }
}