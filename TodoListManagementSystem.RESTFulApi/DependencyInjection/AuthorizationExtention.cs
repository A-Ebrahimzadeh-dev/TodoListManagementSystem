namespace TodoListManagementSystem.RESTFulApi.DependencyInjection
{
    public static class AuthorizationExtention
    {
        public static IServiceCollection AddAuthorizationServices(this IServiceCollection services) 
        {
            services.AddAuthorization();
            return services;
        }
    }
}
