using Aplication.Features.Categorias.Commands.CreateCategoriaCommand;
using Aplication.Features.Categorias.Commands.DeleteCategoriaCommand;
using Aplication.Features.Categorias.Commands.EditCategoriaCommand;
using Aplication.Features.Categorias.Queries.GetAllClientes;
using Aplication.Features.Categorias.Queries.GetClienteId;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoriaProductoController : BaseApiController
    {
        // Get api/v1.0/<controller>
        [HttpGet()]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetAllCategoriaParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQueryPaging
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Codigo = filter.Codigo,
                Nombre = filter.Nombre
            }));
        }

        // Get api/v1.0/<controller>/23123-123123-xvvvs
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetClienteByIdQuery { CategoriaProductoId = id }));
        }
        // Post Api/v1.0/<Controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoriaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //Put api/v1.0/<controller>/5asda-asdas4-asdw4
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EditCategoriaCommand command)
        {
            if (id != command.CategoriaProductoId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        //Delete api/v1.0/<controller>/5asda-asdas4-asdw4
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCategoriaCommand { CategoriaProductoId = id }));
        }


    }
}
