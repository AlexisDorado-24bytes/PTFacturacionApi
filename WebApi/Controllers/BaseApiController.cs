using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Controllers
{
    [ApiController]
    //Versionando controladores
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        //Inyeccion directa del Mediator para que cualquier cosa que herede de BaseApiController lo pueda implementar en el controlador
        protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

    }
}
