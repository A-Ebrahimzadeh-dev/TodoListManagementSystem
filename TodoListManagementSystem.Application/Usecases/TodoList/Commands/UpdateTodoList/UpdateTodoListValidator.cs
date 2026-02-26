using FluentValidation;

namespace TodoListManagementSystem.Application.Usecases.TodoList.Commands.UpdateTodoList
{
    public class UpdateTodoListValidator : AbstractValidator<UpdateTodoListCommand>
    {
        public UpdateTodoListValidator()
        {
            RuleFor(x => x.TodoListId)
                .NotEmpty().WithMessage("Todo list ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.TodoListId)
                .NotEmpty().WithMessage("Todo list ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("The task item title cannot be empty.")
                .Length(5, 20).WithMessage("The task item title must be between 5 and 20 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("The task item descriptions should not exceed 500 characters.");
        }
    }
}
