using Aplication.Especifications;
using Aplication.Interfaces;
using Aplication.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.DetalleFacturaProductos.Commands.DeleteDetalleFacturaCommand
{
    public class DeleteInvoiceDetailMovement : IRequest<Response<Guid>>
    {
        public Guid DetalleFacturaProductoId { get; set; }
    }

    public class DeleteInvoiceDetailMovementHandler : IRequestHandler<DeleteInvoiceDetailMovement, Response<Guid>>
    {
        private readonly IRepositoryAsync<DetalleFacturaProducto> _repositoryAsyncDetalleProducto;
        private readonly IRepositoryAsync<Producto> _repositoryAsyncProducto;
        private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;


        //Creamos constructor para la inyeccion de dependencias
        public DeleteInvoiceDetailMovementHandler(IRepositoryAsync<DetalleFacturaProducto> repositoryAsyncDetalleProducto, IRepositoryAsync<Producto> repositoryAsyncProducto, IRepositoryAsync<Factura> repositoryAsyncFactura)
        {
            _repositoryAsyncDetalleProducto = repositoryAsyncDetalleProducto;
            _repositoryAsyncProducto = repositoryAsyncProducto;
            _repositoryAsyncFactura = repositoryAsyncFactura;
        }


        public async Task<Response<Guid>> Handle(DeleteInvoiceDetailMovement request, CancellationToken cancellationToken)
        {
            //Buscamos el producto para eliminarlo de los movimientos asociados a la factura detalle
            var movimiento = await _repositoryAsyncDetalleProducto.GetByIdAsync(request.DetalleFacturaProductoId);

            //Validamos existencia del movimiento
            if (movimiento == null)
            {
                throw new KeyNotFoundException($"No se encuentra movimiento con el id {request.DetalleFacturaProductoId} proporconado.");
            }
            else
            {
                ////Antes de eiminar movimiento, regresamos las unidades al stock y restamos cantidad y precio de la factura////////

                /////////Buscamos producto pra incrementar el stock///////////
                var productoBd = await _repositoryAsyncProducto.GetByIdAsync(movimiento.ProductoId);

                //Regresamos el stock
                productoBd.Stock = productoBd.Stock + movimiento.Cantidad;

                //Calculamos el valor a restar de la facura
                var valorRestarFactura = productoBd.PrecioVentaCliente * movimiento.Cantidad;


                ///////Consultamos la factura para restar cantidad y precio/////////
                var facturaBd = await _repositoryAsyncFactura.ListAsync(new CustomSearchesFacturas(movimiento.Codigo));

                //Restando valor de factura
                facturaBd[0].TotalProductosVenta = facturaBd[0].TotalProductosVenta - movimiento.Cantidad;

                //Restand calidad de factura
                facturaBd[0].ValorTotalVenta = facturaBd[0].ValorTotalVenta - valorRestarFactura;

                /////////// ACTUALIZAMOS LOS REGISTROS ///////
                await _repositoryAsyncProducto.UpdateAsync(productoBd);
                await _repositoryAsyncFactura.UpdateAsync(facturaBd[0]);

                //Eliminamos movimiento
                await _repositoryAsyncDetalleProducto.DeleteAsync(movimiento);

                return new Response<Guid>(movimiento.DetalleFacturaProductoId);
            }
        }
    }
}
