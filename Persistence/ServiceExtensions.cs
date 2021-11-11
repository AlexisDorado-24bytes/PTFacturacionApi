using Aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuracion)
        {

            services.AddDbContext<FacturacionDbContext>(options =>
            options.UseSqlServer(configuracion.GetConnectionString("ConexionBD"),
            b => b.MigrationsAssembly(typeof(FacturacionDbContext).Assembly.FullName)));

            //Matriculamos todos los repositorios
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));

            #endregion


        }
    }
}
