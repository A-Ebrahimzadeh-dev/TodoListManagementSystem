using System.Text.Json.Serialization;
using TodoListManagementSystem.Domain.Abstractions;
using TodoListManagementSystem.Domain.Exceptions;

namespace TodoListManagementSystem.Domain.Entities
{
    public class TodoList : IEntity
    {
        [JsonConstructor]
        public TodoList(
            Guid id,
            Guid creator,
            string title,
            string? description,
            DateTime createdDate,
            DateTime lastModifiedDate)
        {
            Id = id;
            Creator = creator;
            Title = title;
            Description = description;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
        }
        private TodoList(
            Guid creator,
            string title,
            string? description)
        {
            Id = Guid.CreateVersion7();
            Creator = creator;
            Title = title;
            Description = description;
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid Creator { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }

        public static TodoList CreateNewTodoList(Guid creator,
                                                 string title,
                                                 string? description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullOrEmptyException("Title must have a value.");
            return new(creator, title, description);
        }

        public void UpdateLastModifiedDate() => LastModifiedDate = DateTime.UtcNow;

        public void UpdateContent(string title, string? description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullOrEmptyException("Title must have a value.");
            Title = title;
            Description = description;
        }
    }
}