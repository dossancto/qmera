using FluentValidation;

using QmeraApi.Modules.Todos.Models;

namespace QmeraApi.Modules.Todos.Endpoints.Validations;

public class PostTodoValidation : AbstractValidator<TodoModel>
{
    public PostTodoValidation()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1024);
        RuleFor(x => x.DueDate).NotEmpty().GreaterThan(DateTime.UtcNow);
    }
}