using System.Net.Http.Json;

using QmeraApi.Modules.Todos.Models;
using QmeraApi.Tests.Integration.Configuration;

namespace QmeraApi.Tests.Integration.Modules.Todos.Tests;

public class ListTodosTest(AppFactory<Program> appFactory) : IClassFixture<AppFactory<Program>>
{
    private readonly AppFactory<Program> _appFactory = appFactory;

    [Fact]
    public async Task ListTodos_ShouldReturnTodosAsync()
    {
        var client = _appFactory.CreateClient();

        var createResponse = await client.PostAsJsonAsync("/todos", new
        {
            Title = "Test todo",
            Description = "Test todo description"
        }, cancellationToken: TestContext.Current.CancellationToken);

        createResponse.EnsureSuccessStatusCode();

        var response = await client.GetAsync("/todos", cancellationToken: TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();

        var todos = await response.Content.ReadFromJsonAsync<List<TodoModel>>(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(todos);
        todos.Count.ShouldBeGreaterThan(1);
    }
}