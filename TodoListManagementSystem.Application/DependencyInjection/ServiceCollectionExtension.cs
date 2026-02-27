using Microsoft.Extensions.DependencyInjection;
using TodoListManagementSystem.Application.Abstractions.Utilities;
using TodoListManagementSystem.Application.Universal.Utilities;

namespace TodoListManagementSystem.Application.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddApplicationServices()
            {
                services.AddSingleton<IHashManager, HashManager>();

                return services;
            }
        }
    }
}
