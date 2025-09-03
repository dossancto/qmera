using System.Net.Http.Json;

using QmeraApi.Modules.Todos.Models;

namespace QmeraApi.Tests.Integration.Modules.Todos.Tests;

public class DeleteTodoTest(AppFactory<Program> appFactory) : IClassFixture<AppFactory<Program>>
{
    private readonly AppFactory<Program> _appFactory = appFactory;

    [Fact]
    public async Task DeleteTodo_ShouldDeleteTodoAsync()
    {
        var client = _appFactory.CreateClient();

        var createResponse = await client.PostAsJsonAsync("/todos", new
        {
            Title = "Test todo",
            Description = "Test todo description"
        }, cancellationToken: TestContext.Current.CancellationToken);

        createResponse.EnsureSuccessStatusCode();

        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoModel>(cancellationToken: TestContext.Current.CancellationToken);

        var response = await client.GetAsync($"/todos/{createdTodo!.Id}", cancellationToken: TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();

        var todoDetail = await response.Content.ReadFromJsonAsync<TodoModel>(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(todoDetail);

        var deleteResponse = await client.DeleteAsync($"/todos/{todoDetail.Id}", cancellationToken: TestContext.Current.CancellationToken);

        deleteResponse.EnsureSuccessStatusCode();
    }
}