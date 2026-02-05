using Microsoft.Extensions.DependencyInjection;
using TodoListManagementSystem.Application.Abstractions.Utilities;
using TodoListManagementSystem.Application.Universal.Utilities;

namespace TodoListManagementSystem.Application.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHashManager, HashManager>();

            return services;
        }
    }
}
