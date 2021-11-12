using Aplication.Features.Facturas.Queries.CustomSearches;
using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.DetalleFacturaProductos.Commands.CreateFacturaCommand
{
    public class CreateDetalleFacturaProductosCommand : IRequest<Response<Guid>>
    {
        public Guid Codigo { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }


        public class CreateDetalleFacturaProductosCommandHandler : IRequestHandler<CreateDetalleFacturaProductosCommand, Response<Guid>>
        {

            private readonly IRepositoryAsync<DetalleFacturaProducto> _repositoryAsync;
            private readonly IRepositoryAsync<Producto> _repositoryAsyncProducto;
            private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;

            private readonly IMapper _mapper;

            public CreateDetalleFacturaProductosCommandHandler(IRepositoryAsync<Factura> repositoryAsyncFactura, IRepositoryAsync<Producto> repositoryAsyncProducto, IRepositoryAsync<DetalleFacturaProducto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncProducto = repositoryAsyncProducto;
                _repositoryAsyncFactura = repositoryAsyncFactura;
                _mapper = mapper;
            }

            public async Task<Response<Guid>> Handle(CreateDetalleFacturaProductosCommand request, CancellationToken cancellationToken)
            {

                await ExtisteProducto(request.ProductoId);
                await ExtisteFactura(request.Codigo);

                var producto = await _repositoryAsyncProducto.GetByIdAsync(request.ProductoId);
                request.ValorTotal = producto.PrecioVentaCliente * request.Cantidad;
                request.ValorUnitario = producto.PrecioVentaCliente;

                var nuevoRegistro = _mapper.Map<DetalleFacturaProducto>(request);

                var data = await _repositoryAsync.AddAsync(nuevoRegistro);

                //Una vez se guarda, pasamos a actualizar el valor total de la factura
                await ActualizarTotalFactutaYCantidad(request.Codigo, request.ValorTotal);

                return new Response<Guid>(data.DetalleFacturaProductoId);
            }

            private async Task<List<Factura>> ActualizarTotalFactutaYCantidad(Guid codigoFactura, decimal valorTotal)
            {
                var factura = await _repositoryAsyncFactura.ListAsync(new CustomSearchesFacturas(codigoFactura));
                factura[0].ValorTotalVenta = factura[0].ValorTotalVenta + valorTotal;

                await _repositoryAsyncFactura.UpdateAsync(factura[0]);

                return factura;

            }
            private async Task ExtisteFactura(Guid codido)
            {

                var producto = await _repositoryAsyncFactura.ListAsync(new CustomSearchesFacturas(codido));

                if (producto == null)
                {
                    throw new KeyNotFoundException($"No existe Factura  con el codigo {codido} proporconado.");
                }
            }
            private async Task ExtisteProducto(Guid productoId)
            {

                var producto = await _repositoryAsyncProducto.GetByIdAsync(productoId);

                if (producto == null)
                {
                    throw new KeyNotFoundException($"No existe producto con el id {productoId} proporconado.");
                }
            }


        }
    }

}
