using Aplication.Behavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aplication
{
    //Nos permite agrupar las inyecciones de nuestros servicios o de terceros, ya que tenemos desacoplada toda la api
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            //Matriculando el automapper, para que automaticamente  registre los mapeos en ésta biblioteca de cllases.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Matriculando el FluentValidation y poder usar las validaiones.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Matriculamos para usaar el patron mediator.
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Matriculamos el pepeline que valida el flujo del patron mediator, intercepta y valida los datos recibidos.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



        }
    }
}
