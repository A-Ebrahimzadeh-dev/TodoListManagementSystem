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

        public async Task<TaskItemDto> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var todoListExists = await _todoListRepository.ExistsAsync(request.UserId, request.TodoListId, cancellationToken);
            if (!todoListExists)
                throw new SourceNotFoundException($"Todo list with ID [{request.TodoListId}] not found for user [{request.UserId}].");

            var taskItemExists = await _taskItemRepository.ExistsAsync(request.UserId, request.TodoListId, request.Title, request.DueDate, cancellationToken);
            if (taskItemExists)
                throw new SourceAlreadyExistsException($"Task item with title '{request.Title}' and due date '{request.DueDate:yyyy-MM-dd}' already exists.");

            var taskItem = Domain.Entities.TaskItem.CreateNewTaskItem(
                request.TodoListId,
                request.Title,
                request.Description,
                request.PriorityLevel,
                request.DueDate,
                request.Tags);

            await _taskItemRepository.SaveAsync(request.UserId, taskItem, cancellationToken);

            return taskItem.MapToTaskItemDto();
        }
    }
}
