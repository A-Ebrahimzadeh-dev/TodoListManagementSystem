using FluentValidation;
using TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTags;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.UpdateTags
{
    public class UpdateTagsValidator : AbstractValidator<UpdateTagsCommand>
    {
        public UpdateTagsValidator()
        {
            RuleFor(x => x.NewTags)
                .NotNull().WithMessage("The list of tags cannot be null.")
                .Must(tags => tags == null || tags.All(t => !string.IsNullOrWhiteSpace(t)))
                    .WithMessage("Tags cannot be empty or just spaces.")
                .Must(tags => tags!.Distinct().Count() == tags!.Count)
                    .WithMessage("There are duplicate tags.");
        }
    }
}
