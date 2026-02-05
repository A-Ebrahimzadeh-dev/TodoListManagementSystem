using System.Text.Json.Serialization;
using TodoListManagementSystem.Domain.Abstractions;

namespace TodoListManagementSystem.Domain.Entities
{
    public sealed class User : IEntity
    {
        [JsonConstructor]
        public User(
            Guid id,
            string firstname,
            string lastname,
            string username,
            string email,
            string password,
            DateTime updatedAt,
            DateTime createdAt)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Password = password;
            UpdatedAt = updatedAt;
            CreatedAt = createdAt;
        }
        private User(
            string firstname,
            string lastname,
            string username,
            string email,
            string password,
            DateTime updatedAt,
            DateTime createdAt)
        {
            Id = Guid.CreateVersion7();
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Password = password;
            UpdatedAt = updatedAt;
            CreatedAt = createdAt;
        }

        public Guid Id { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public static User CreateNewUser(string firstname,
                                         string lastname,
                                         string username,
                                         string email,
                                         string password)
        {
            return new(firstname, lastname, username, email, password, DateTime.UtcNow, DateTime.UtcNow);
        }
    }
}
