using QmeraApi.Modules.Todos.Endpoints;

namespace QmeraApi.Modules.Todos;

public static class TodosModule
{
    public static IServiceCollection AddTodosModule(this IServiceCollection services)
    {
        return services;
    }

    public static void UseTodosModule(this WebApplication app)
    {
        app.MapTodoEndpoints();
    }
}