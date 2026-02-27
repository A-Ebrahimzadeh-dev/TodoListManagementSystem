using System.Collections.Concurrent;
using System.Text.Json;
using TodoListManagementSystem.Domain.Abstractions;
using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.Infrastructure.Persistence.Data.FileSystem
{
    public sealed class JsonFileStore : IJsonFileStore
    {
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public async Task<T?> ReadAsync<T>(
            string filePath,
            CancellationToken cancellationToken) where T : IEntity
        {
            if (!File.Exists(filePath))
                return default;

            var fileLock = GetLock(filePath);

            await fileLock.WaitAsync(cancellationToken);
            try
            {
                await using var stream = new FileStream(
                    filePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read,
                    bufferSize: 4096,
                    useAsync: true);

                return await JsonSerializer.DeserializeAsync<T>(
                    stream,
                    _jsonOptions,
                    cancellationToken);
            }
            finally
            {
                fileLock.Release();
            }
        }

        public async Task<IReadOnlyList<T>> ReadAllFromDirectoryAsync<T>(
            string directoryPath,
            CancellationToken cancellationToken) where T : IEntity
        {
            if (!Directory.Exists(directoryPath))
                return [];

            var files = Directory.GetFiles(directoryPath, "*.json");

            var results = new List<T>(files.Length);

            foreach (var filePath in files)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var fileLock = GetLock(filePath);

                await fileLock.WaitAsync(cancellationToken);
                try
                {
                    await using var stream = new FileStream(
                        filePath,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read,
                        bufferSize: 4096,
                        useAsync: true);

                    var entity = await JsonSerializer.DeserializeAsync<T>(
                        stream,
                        _jsonOptions,
                        cancellationToken);

                    if (entity is not null)
                        results.Add(entity);
                }
                finally
                {
                    fileLock.Release();
                }
            }

            return results;
        }

        public async Task WriteAsync<T>(
            string filePath,
            T data,
            CancellationToken cancellationToken) where T : IEntity
        {
            var fileLock = GetLock(filePath);

            await fileLock.WaitAsync(cancellationToken);
            try
            {
                var dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrWhiteSpace(dir))
                    Directory.CreateDirectory(dir);

                var tempFilePath = filePath + ".tmp";

                await using (var stream = new FileStream(
                    tempFilePath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    await JsonSerializer.SerializeAsync(
                        stream,
                        data,
                        _jsonOptions,
                        cancellationToken);
                }

                File.Move(tempFilePath, filePath, overwrite: true);
            }
            finally
            {
                fileLock.Release();
            }
        }

        public async Task DeleteAsync(
            string filePath,
            CancellationToken cancellationToken)
        {
            var fileLock = GetLock(filePath);

            await fileLock.WaitAsync(cancellationToken);
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            finally
            {
                fileLock.Release();
            }
        }

        public Task<bool> ExistsAsync(
            string filePath,
            CancellationToken cancellationToken)
        {
            //var r = Directory.Exists(filePath);
            return Task.FromResult(File.Exists(filePath));
        }

        private static SemaphoreSlim GetLock(string path)
        {
            return _locks.GetOrAdd(
                path,
                _ => new SemaphoreSlim(1, 1));
        }
    }
}
