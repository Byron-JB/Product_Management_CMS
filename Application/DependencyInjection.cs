using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;
using MediatR;
using FluentValidation;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services
            services.AddInfrastructureServices(configuration);

            // Register MediatR for CQRS handlers in this assembly (vertical slice handlers live next to requests)
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            // Register FluentValidation validators from this assembly
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            // No-op update to trigger update

            // Register MediatR pipeline behaviours: validation and logging
            services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(Application.Behaviors.ValidationBehavior<,>));
            services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(Application.Behaviors.LoggingBehavior<,>));
            // Activity behavior (records Create/Update/Delete)
            services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(Application.Behaviors.ActivityBehavior<,>));

            return services;
        }
    }
}
