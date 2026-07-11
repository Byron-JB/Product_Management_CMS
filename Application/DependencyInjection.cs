using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services
            services.AddInfrastructureServices(configuration);

            // Register application layer services here (e.g., use cases, handlers)

            return services;
        }
    }
}
