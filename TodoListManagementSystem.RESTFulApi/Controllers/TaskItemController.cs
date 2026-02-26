using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Application.Usecases.TaskItem.Queries.GetTaskItemById;
using TodoListManagementSystem.RESTFulApi.Extensions.Mappers;
using TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem;

namespace TodoListManagementSystem.RESTFulApi.Controllers
{
    [Authorize]
    public class TaskItemsController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{taskItemId:Guid}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskItemById([FromQuery] Guid todoListId, Guid taskItemId)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var results = await _mediator.Send(new GetTaskItemByIdQuery(userId, todoListId, taskItemId));

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewTaskItem([FromBody] CreateTaskItemRequest request)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = request.MapToCreateTaskItemCommand(userId);

            var taskItem = await _mediator.Send(command);

            return Ok(taskItem);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateTaskItem([FromBody] UpdateTaskItemRequest request, Guid id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = request.MapToUpdateTaskItemCommand(userId, id);

            var taskItem = await _mediator.Send(command);

            return Ok(taskItem);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteTaskItem([FromBody] DeleteTaskItemRequest request, Guid id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = request.MapToDeleteTaskItemCommand(userId, id);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{id:Guid}/Tags")]
        public async Task<ActionResult> UpdateTaskItemTags([FromBody] UpdateTaskItemTagsRequest request, Guid id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = request.MapToTaskItemTagsCommand(userId, id);

            var taskItem = await _mediator.Send(command);

            return Ok(taskItem);
        }

        [HttpPatch("{id:Guid}/Status")]
        public async Task<ActionResult> UpdateTaskItemStatus([FromBody] UpdateTaskItemStatusRequest request, Guid id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = request.MapToChangeTaskItemStatusCommand(userId, id);

            var taskItem = await _mediator.Send(command);

            return Ok(taskItem);
        }

        [HttpPatch("{id:Guid}/Priority")]
        public async Task<ActionResult> UpdateTaskItemPriorityLevel([FromBody] UpdateTaskItemPriorityLevelRequest request, Guid id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = request.MapToChangePriorityLevelCommand(userId, id);

            var taskItem = await _mediator.Send(command);

            return Ok(taskItem);
        }
    }
}
