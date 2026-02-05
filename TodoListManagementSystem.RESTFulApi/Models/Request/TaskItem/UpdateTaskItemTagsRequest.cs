namespace TodoListManagementSystem.RESTFulApi.Models.Request.TaskItem
{
    public record UpdateTaskItemTagsRequest(Guid TodoListId, List<string>? NewTags);
}
