using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using QmeraApi.Modules.Commum.Adapters.Databases;
using QmeraApi.Modules.Todos.Models;

namespace QmeraApi.Modules.Todos.Endpoints;

public static class TodoEndpointsExtension
{
    public static void MapTodoEndpoints(this WebApplication app)
    {
        app.MapGet("/todos", async (
                    [FromServices] ApplicationDbContext db) =>
        {
            var todos = await db.Todos.ToListAsync();

            return todos;
        });

        app.MapGet("/todos/{id}", async (
                    [FromServices] ApplicationDbContext db,
                    [FromRoute] int id) =>
        {
            var todo = await db.Todos.FindAsync(id);

            return todo;
        });

        app.MapPost("/todos", async (
                    [FromServices] ApplicationDbContext db,
                    [FromBody] TodoModel todo) =>
        {
            db.Todos.Add(todo);

            await db.SaveChangesAsync();

            return todo;
        });

        app.MapPut("/todos/{id}", async (
                    [FromServices] ApplicationDbContext db,
                    [FromRoute] int id,
                    [FromBody] TodoModel todo
                ) =>
        {
            var todoToUpdate = await db.Todos.FindAsync(id);

            if (todoToUpdate is null)
            {
                return Results.NotFound();
            }

            todoToUpdate.Title = todo.Title;
            todoToUpdate.Description = todo.Description;
            todoToUpdate.DueDate = todo.DueDate;
            todoToUpdate.IsCompleted = todo.IsCompleted;

            await db.SaveChangesAsync();

            return Results.Ok(todoToUpdate);
        });

        app.MapDelete("/todos/{id}", async (
                    [FromServices] ApplicationDbContext db,
                    [FromRoute] int id) =>
        {
            var todoToDelete = await db.Todos.FindAsync(id);

            if (todoToDelete is null)
            {
                return Results.NotFound();
            }

            db.Todos.Remove(todoToDelete);

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

    }
}