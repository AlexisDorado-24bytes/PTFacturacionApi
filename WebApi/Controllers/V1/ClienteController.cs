using Aplication.Features.Clientes.Queries.GetAllClientesQueries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ClienteController : BaseApiController
    {
        // Get api/v1.0/<controller>
        [HttpGet()]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetAllClientesParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllClientes
            {
                CedulaCuidadania = filter.CeludaDeCiudadania,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            }));
        }

        // Get api/v1.0/<controller>/23123-123123-xvvvs
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(Guid id)
        //{
        //    return Ok(await Mediator.Send(new GetAllProductosCommands { FacturaId = id }));
        //}
        // Post Api/v1.0/<Controller>
        //[HttpPost]
        //public async Task<IActionResult> Post(CreatedProductoCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

        //Put api/v1.0/<controller>/5asda-asdas4-asdw4
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, EditCategoriaCommand command)
        //{
        //    if (id != command.FacturaId)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(await Mediator.Send(command));
        //}

        ////Delete api/v1.0/<controller>/5asda-asdas4-asdw4
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    return Ok(await Mediator.Send(new DeleteCategoriaCommand { FacturaId = id }));
        //}


    }
}
