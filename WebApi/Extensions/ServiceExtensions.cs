using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AppApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                //Se configura para que cuando el cliente no especifique la versión se tome por defauld la 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
