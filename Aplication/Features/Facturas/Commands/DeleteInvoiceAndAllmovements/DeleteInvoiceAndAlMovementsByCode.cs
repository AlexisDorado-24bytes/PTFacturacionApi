using Aplication.Especifications;
using Aplication.Interfaces;
using Aplication.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Facturas.Commands.DeleteInvoiceAndAllmovements
{
    public class DeleteInvoiceAndAlMovementsByCode : IRequest<Response<Guid>>
    {
        public Guid CodigoUnicoDeFactura { get; set; }

        public class DeleteInvoiceAndAlMovementsByCodeHandler : IRequestHandler<DeleteInvoiceAndAlMovementsByCode, Response<Guid>>
        {
            private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
            private readonly IRepositoryAsync<DetalleFacturaProducto> _repositoryAsyncFacturDetallea;
            private readonly IRepositoryAsync<Producto> _repositoryAsyncProducto;

            public DeleteInvoiceAndAlMovementsByCodeHandler(IRepositoryAsync<Factura> repositoryAsyncFactura, IRepositoryAsync<DetalleFacturaProducto> repositoryAsyncFacturDetallea, IRepositoryAsync<Producto> repositoryAsyncProducto)
            {
                _repositoryAsyncFactura = repositoryAsyncFactura;
                _repositoryAsyncFacturDetallea = repositoryAsyncFacturDetallea;
                _repositoryAsyncProducto = repositoryAsyncProducto;
            }


            public async Task<Response<Guid>> Handle(DeleteInvoiceAndAlMovementsByCode request, CancellationToken cancellationToken)
            {
                var factura = await _repositoryAsyncFactura.ListAsync(new CustomSearchesFacturas(request.CodigoUnicoDeFactura));
                var detalleFactura = await _repositoryAsyncFacturDetallea.ListAsync(new SearchAllFacturasDetalleAndByCodigo(request.CodigoUnicoDeFactura));

                await _repositoryAsyncFactura.DeleteAsync(factura[0]);

                foreach (var item in detalleFactura)
                {
                    var producto = await _repositoryAsyncProducto.GetByIdAsync(item.ProductoId);
                    producto.Stock = producto.Stock + item.Cantidad;
                    await _repositoryAsyncProducto.UpdateAsync(producto);
                    await _repositoryAsyncFacturDetallea.DeleteAsync(item);

                }

                return new Response<Guid>(factura[0].FacturaId);
            }

        }

    }
}
