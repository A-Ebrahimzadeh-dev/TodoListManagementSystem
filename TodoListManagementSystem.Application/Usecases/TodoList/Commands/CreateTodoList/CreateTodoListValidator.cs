using FluentValidation;
using TodoListManagementSystem.Application.Usecases.TodoList.Commands.CreateTodoList;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commads.CreateTodoList
{
    public class CreateTodoListValidator : AbstractValidator<CreateTodoListCommand>
    {
        public CreateTodoListValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("The task item title cannot be empty.")
                .Length(5, 20).WithMessage("The task item title must be between 5 and 20 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("The task item descriptions should not exceed 500 characters.");
        }
    }
}
