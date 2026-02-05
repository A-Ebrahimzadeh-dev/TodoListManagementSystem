using TodoListManagementSystem.Application.DependencyInjection;
using TodoListManagementSystem.Infrastructure.Persistence.DependencyInjection;
using TodoListManagementSystem.RESTFulApi.DependencyInjection;
using TodoListManagementSystem.RESTFulApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
