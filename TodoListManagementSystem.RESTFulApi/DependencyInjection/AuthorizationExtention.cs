namespace TodoListManagementSystem.RESTFulApi.DependencyInjection
{
    public static class AuthorizationExtention
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAuthorizationServices()
            {
                services.AddAuthorization();
                return services;
            }
        }
    }
}
