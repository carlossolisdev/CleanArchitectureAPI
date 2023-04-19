using CleanAPI.Application.Interfaces.Persistence;
using CleanAPI.Infraestructure.Persistence.Beers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanAPI.Infraestructure
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("CleanAPI");
            services.AddDbContext<BeersDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddScoped<IBeersDbContext, BeersDbContext>();

            return services;
        }
    }
}
