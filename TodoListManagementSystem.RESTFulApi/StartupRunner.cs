using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;

namespace TodoListManagementSystem.RESTFulApi
{
    public sealed class StartupRunner(
        IFileSystemInitializer initializer,
        ILogger<StartupRunner> logger) : IHostedService
    {
        private readonly IFileSystemInitializer _initializer = initializer;
        private readonly ILogger<StartupRunner> _logger = logger;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Initializing file system...");
                await _initializer.InitializeAsync(cancellationToken);
                _logger.LogInformation("File system initialized successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "File system initialization failed.");
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
