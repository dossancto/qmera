using System.Net.Http.Json;

using QmeraApi.Tests.Integration.Configuration;

namespace QmeraApi.Tests.Integration.Modules.Todos.Tests;

public class CreateTodoTest(AppFactory<Program> appFactory) : IClassFixture<AppFactory<Program>>
{
    private readonly AppFactory<Program> _appFactory = appFactory;

    [Fact]
    public async Task CreateTodo_ShouldReturnCreatedTodo()
    {
        var client = _appFactory.CreateClient();

        var response = await client.PostAsJsonAsync("/todos", new
        {
            Title = "Test todo",
            Description = "Test todo description"
        }, cancellationToken: TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();
    }
}