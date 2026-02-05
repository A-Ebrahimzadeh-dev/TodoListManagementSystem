namespace TodoListManagementSystem.Application.Abstractions.Utilities
{
    public interface IHashManager
    {
        public string Create(string value);
        public bool Verify(string hashedValue, string value);
    }
}
