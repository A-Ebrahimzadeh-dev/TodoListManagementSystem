using FluentValidation;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.CreateTaskItem
{
    public class CreateTaskItemValidator : AbstractValidator<CreateTaskItemCommand>
    {
        public CreateTaskItemValidator()
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

            RuleFor(x => x.DueDate)
                .Must(date => date == null || date.Value >= DateTime.UtcNow)
                .WithMessage("The task item due date cannot be in the past.");

            RuleFor(x => x.Tags)
                .NotNull().WithMessage("The list of tags cannot be null.")
                .Must(tags => tags == null || tags.All(t => !string.IsNullOrWhiteSpace(t)))
                    .WithMessage("Tags cannot be empty or just spaces.")
                .Must(tags => tags!.Distinct().Count() == tags!.Count)
                    .WithMessage("There are duplicate tags.");
        }
    }
}
