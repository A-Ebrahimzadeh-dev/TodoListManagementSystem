using MediatR;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Application.Exceptions;
using TodoListManagementSystem.Application.Extensions.Mappers;
using TodoListManagementSystem.Domain.Abstractions.Repositories;

namespace TodoListManagementSystem.Application.Usecases.TaskItem.Commands.CreateTaskItem
{
    public class CreateTaskItemCommandHandler(
        ITodoListRepository todoListRepository,
        ITaskItemRepository taskItemRepository) : IRequestHandler<CreateTaskItemCommand, TaskItemDto>
    {
        private readonly ITodoListRepository _todoListRepository = todoListRepository;
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;

        public async Task<TaskItemDto> Handle(CreateTaskItemCommand command, CancellationToken cancellationToken)
        {
            await ValidateAccessAsync(command, cancellationToken);

            var existingTaskItem = await _taskItemRepository.ExistsAsync(command.Title, command.DueDate, cancellationToken);
            if (existingTaskItem)
                throw new SourceAlreadyExistsException($"Task item with title '{command.Title}' and due date '{command.DueDate:yyyy-MM-dd}' already exists.");

            var taskItem = Domain.Entities.TaskItem.CreateNewTaskItem(
                command.TodoListId,
                command.Title,
                command.Description,
                command.PriorityLevel,
                command.DueDate,
                command.Tags);

            var newTaskItem = await _taskItemRepository.SaveAsync(taskItem, cancellationToken);

            return newTaskItem.MapToTaskItemDto();
        }

        private async Task ValidateAccessAsync(CreateTaskItemCommand command, CancellationToken ct)
        {
            var existingTodoList = await _todoListRepository.GetByIdAsync(command.TodoListId, ct)
                ?? throw new SourceNotFoundException($"Todo list with ID [{command.TodoListId}] not found.");
            if (existingTodoList.Creator != command.UserId)
                throw new AccessDeniedException($"User [{command.UserId}] not have permission to create this task item.");
        }
    }
}
