using System.Text.Json.Serialization;
using TodoListManagementSystem.Domain.Abstractions;
using TodoListManagementSystem.Domain.Exceptions;

namespace TodoListManagementSystem.Domain.Entities
{
    public class TaskItem : IEntity
    {
        [JsonConstructor]
        public TaskItem(
            Guid id,
            Guid todoListId,
            string title,
            string? description,
            PriorityLevel priorityLevel,
            Status status,
            DateTime? dueDate,
            List<string>? tags,
            DateTime createdDate,
            DateTime lastModifiedDate,
            DateTime completedDate)
        {
            Id = id;
            TodoListId = todoListId;
            Title = title;
            Description = description;
            Priority = priorityLevel;
            Status = status;
            DueDate = dueDate;
            Tags = tags;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            CompletedDate = completedDate;
        }
        private TaskItem(Guid todoListId,
                         string title,
                         string? description,
                         PriorityLevel priorityLevel,
                         Status status,
                         DateTime? dueDate,
                         List<string>? tags)
        {
            Id = Guid.CreateVersion7();
            TodoListId = todoListId;
            Title = title;
            Description = description;
            Priority = priorityLevel;
            Status = status;
            DueDate = dueDate;
            Tags = tags;
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid TodoListId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public PriorityLevel Priority { get; private set; }
        public Status Status { get; private set; }
        public DateTime? DueDate { get; private set; }
        public List<string>? Tags { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public DateTime? CompletedDate { get; private set; }

        public static TaskItem CreateNewTaskItem(Guid todoListId,
                                                 string title,
                                                 string? description,
                                                 PriorityLevel priorityLevel,
                                                 DateTime? dueDate,
                                                 List<string>? tags)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullOrEmptyException("Title must have a value");
            return new(todoListId, title, description, priorityLevel, Status.InProgress, dueDate, tags);
        }

        public void ChangeStatus(Status newStatus)
        {
            Status = newStatus;
            CompletedDate = newStatus == Status.Completed ? DateTime.UtcNow : null;
        }

        public void ChangePriorityLevel(PriorityLevel newPriorityLevel)
        {
            Priority = newPriorityLevel;
        }

        public void UpdateLastModifiedDate() => LastModifiedDate = DateTime.UtcNow;

        public void UpdateTagsList(List<string>? tags)
        {
            var currentTags = this.Tags ?? [];
            var newTags = tags ?? [];

            var tagsToAdd = newTags.Except(currentTags, StringComparer.Ordinal).ToList();
            var tagsToRemove = currentTags.Except(newTags, StringComparer.Ordinal).ToList();

            foreach (var tag in tagsToRemove)
            {
                this.DeleteTag(tag);
            }

            foreach (var tag in tagsToAdd)
            {
                this.AddTag(tag);
            }
        }

        public void UpdateContent(string title,
                                  string? description,
                                  PriorityLevel priorityLevel,
                                  Status status,
                                  DateTime? dueDate,
                                  List<string>? tags)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullOrEmptyException("Title must have a value");
            Title = title;
            Description = description;
            Priority = priorityLevel;
            DueDate = dueDate;
            UpdateTagsList(tags);
            ChangeStatus(status);
        }

        private void AddTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullOrEmptyException("Tag must have a value");

            string? existingTag = FindSpecificTag(tag);
            if (existingTag is not null)
                throw new DuplicateValueException($"Tag with value [{tag}] already exist");

            Tags?.Add(existingTag!);
        }

        private void DeleteTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullOrEmptyException("Tag must have a value");

            string? existingTag = FindSpecificTag(tag)
                ?? throw new ValueNotFoundException($"Tags list doesn't have tag with value [{tag}]");

            Tags?.Remove(existingTag);
        }

        private string? FindSpecificTag(string tag)
            => Tags?.FirstOrDefault(t => t.Equals(tag, StringComparison.Ordinal));
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum Status
    {
        InProgress,
        Completed,
        Cancelled
    }
}
