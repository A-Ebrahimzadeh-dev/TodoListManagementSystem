namespace TodoListManagementSystem.Shared.Settings
{
    public class HashSettings
    {
        public int SaltSize { get; set; } = 16;
        public int MemorySizeKB { get; set; } = 65536;
        public int Iterations { get; set; } = 4;
        public int DegreeOfParallelism { get; set; } = 2;
        public int HashLength { get; set; } = 32;
    }
}
