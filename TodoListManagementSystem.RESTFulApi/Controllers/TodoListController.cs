using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TodoListManagementSystem.Application.DTOs.TaskItem;
using TodoListManagementSystem.Application.DTOs.TodoList;
using TodoListManagementSystem.Application.Usecases.TodoList.Commads.DeleteTodoList;
using TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetMyTodoLists;
using TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetMyTodoListTaskItems;
using TodoListManagementSystem.Application.Usecases.TodoList.Queries.GetTodoListById;
using TodoListManagementSystem.RESTFulApi.Extensions.Mappers.TodoList;
using TodoListManagementSystem.RESTFulApi.Models.Request.TodoList;

namespace TodoListManagementSystem.RESTFulApi.Controllers
{
    [Authorize]
    public class TodoListsController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<TodoListDto>>> GetMyTodoLists()
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var results = await _mediator.Send(new GetMyTodoListTaskItemsQuery(userId));

            return Ok(results);
        }

        [HttpGet("{todoListId:Guid}/TaskItems")]
        public async Task<ActionResult<IReadOnlyCollection<TaskItemDto>>> GetMyTodoListTaskItems(Guid todoListId)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var results = await _mediator.Send(new GetMyTodoListsTaskItemsQuery(userId, todoListId));

            return Ok(results);
        }

        [HttpGet("{todoListId:Guid}")]
        public async Task<ActionResult<TodoListDto>> GetTodoListById(Guid todoListId)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var results = await _mediator.Send(new GetTodoListByIdQuery(todoListId, userId));

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewTodoList([FromBody] CreateTodoListRequest request)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = request.MapToCreateTodoListCommand(userId);

            var todoList = await _mediator.Send(command);

            return Ok(todoList);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateTodoList([FromBody] UpdateTodoListRequest request, Guid id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = request.MapToUpdateTodoListCommand(userId, id);

            var taskItem = await _mediator.Send(command);

            return Ok(taskItem);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteTodoList(Guid id)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var command = new DeleteTodoListCommand(userId, id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
