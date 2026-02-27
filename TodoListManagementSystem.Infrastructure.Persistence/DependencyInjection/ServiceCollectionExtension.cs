using Microsoft.Extensions.DependencyInjection;
using TodoListManagementSystem.Domain.Abstractions.Repositories;
using TodoListManagementSystem.Infrastructure.Persistence.Abstractions.Data.FileSystem;
using TodoListManagementSystem.Infrastructure.Persistence.Data.FileSystem;
using TodoListManagementSystem.Infrastructure.Persistence.Data.Repositories;

namespace TodoListManagementSystem.Infrastructure.Persistence.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddInfrastructureServices()
            {
                services.AddSingleton<IPathProvider, PathProvider>();

                services.AddScoped<IJsonFileStore, JsonFileStore>();

                services.AddSingleton<IFileSystemInitializer, FileSystemInitializer>();

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ITodoListRepository, TodoListRepository>();
                services.AddScoped<ITaskItemRepository, TaskItemRepository>();

                return services;
            }
        }
    }
}
