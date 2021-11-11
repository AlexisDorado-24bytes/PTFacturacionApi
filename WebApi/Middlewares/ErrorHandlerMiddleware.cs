using Aplication.Wrappers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Middlewares
{
    //Middleware para interceptar kos errores y dar una respuesta ya planificada
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            //Cuando existe error, lo tomamos y le damos formato
            catch (System.Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new Response<string>() { Succeeded = false, Message = error.Message };

                //Asignando los códigos de error
                switch (error)
                {
                    case Aplication.Exceptions.ApiException e:
                        //Error de validaciones personalizadas de aplicacion
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case Aplication.Exceptions.ValidationException e:
                        //Error de validaciones personalizadas de aplicacion
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;

                    case KeyNotFoundException e:
                        //Error de no encontrado
                        response.StatusCode = (int)HttpStatusCode.NotFound;

                        break;

                    default:
                        //Error no manejado
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsync(JsonSerializer.Serialize(responseModel));

            }
        }
    }
}
