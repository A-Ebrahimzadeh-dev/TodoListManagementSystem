using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;
using TodoListManagementSystem.Application;
using TodoListManagementSystem.Application.Behaviors;
using TodoListManagementSystem.RESTFulApi.Abstractions.Utilities;
using TodoListManagementSystem.RESTFulApi.Utilities;
using TodoListManagementSystem.Shared.Settings;

namespace TodoListManagementSystem.RESTFulApi.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPresentationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationAssembly = typeof(ApplicationAssemblyMarker).Assembly;

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }); ;

            services.Configure<AppSettings>(
                configuration.GetSection(AppSettings.SectionName)
            );

            services.AddHostedService<StartupRunner>();

            services.AddAuthenticationServices(configuration);
            services.AddAuthorizationServices();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter JWT token like: Bearer {your token}"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });


            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddValidatorsFromAssembly(applicationAssembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)
            );

            services.AddSingleton<IAuthTokenProvider, AuthTokenProvider>();

            return services;
        }
    }
}