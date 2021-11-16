using Aplication.Features.Facturas.Commands.CreateFacturaCommand;
using Aplication.Features.Facturas.Commands.DeleteInvoiceAndAllmovements;
using Aplication.Features.Facturas.Queries.GetAllFacturas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    public class FacturaController : BaseApiController
    {
        // Get api/v1.0/<controller>
        [HttpGet()]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetAllFacturasQueryPagingParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllFacturasQueryPaging
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Codigo = filter.Codigo
            }));
        }

        //// Get api/v1.0/<controller>/23123-123123-xvvvs
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(Guid id)
        //{
        //    return Ok(await Mediator.Send(new GetClienteByIdQuery { FacturaId = id }));
        //}
        //// Post Api/v1.0/<Controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateFacturaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

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
        [HttpDelete("{CodigoUnicoDeFactura}")]
        public async Task<IActionResult> Delete(Guid CodigoUnicoDeFactura)
        {
            return Ok(await Mediator.Send(new DeleteInvoiceAndAlMovementsByCode { CodigoUnicoDeFactura = CodigoUnicoDeFactura }));
        }


    }
}
