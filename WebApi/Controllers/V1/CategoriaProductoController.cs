using Aplication.Features.Categorias.Commands.CreateCategoriaCommand;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoriaProductoController : BaseApiController
    {
        // Post Api/v1.0/<Controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoriaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
