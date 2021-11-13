using Aplication.DTOs;
using Aplication.Especifications;
using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Facturas.Queries.ShowInvoiceWithFullSale
{
    public class ShowInvoiceWithFullSale : IRequest<ListaDeFacturas>
    {
        public Guid CodigoUnicoDeFactura { get; set; }


        public class ShowInvoiceWithFullSaleHandler : IRequestHandler<ShowInvoiceWithFullSale, ListaDeFacturas>
        {
            private readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;
            private readonly IRepositoryAsync<DetalleFacturaProducto> _repositoryAsyncFacturaDetalle;
            private readonly IRepositoryAsync<Producto> _repositoryAsyncProducto;
            private readonly IMapper _mapper;


            public ShowInvoiceWithFullSaleHandler(IRepositoryAsync<Factura> repositoryAsyncFactura, IMapper mapper, IRepositoryAsync<DetalleFacturaProducto> repositoryAsyncFacturaDetalle, IRepositoryAsync<Producto> repositoryAsyncProducto)
            {
                _repositoryAsyncFactura = repositoryAsyncFactura;
                _mapper = mapper;
                _repositoryAsyncFacturaDetalle = repositoryAsyncFacturaDetalle;
                _repositoryAsyncProducto = repositoryAsyncProducto;
            }


            public async Task<ListaDeFacturas> Handle(ShowInvoiceWithFullSale request, CancellationToken cancellationToken)
            {
                //Validando existencia de factura
                var factura = await _repositoryAsyncFactura.ListAsync(new SearchOnlyInvoiceWithCode(
                    request.CodigoUnicoDeFactura));

                if (factura.Count == 0)
                {
                    throw new KeyNotFoundException($"No se encuentra factura con el id {request.CodigoUnicoDeFactura} proporconado.");
                }

                var facturaDetalle = await _repositoryAsyncFacturaDetalle.ListAsync(new SearchAllFacturasDetalleAndByCodigo(
                    request.CodigoUnicoDeFactura));

                ListaDeFacturas oListaDeFacturas = new ListaDeFacturas();


                foreach (var item in facturaDetalle)
                {
                    var pro = await _repositoryAsyncProducto.GetByIdAsync(item.ProductoId);
                    var dto = _mapper.Map<ProductoDto>(pro);
                    item.Producto = _mapper.Map<Producto>(dto);
                }

                oListaDeFacturas.Factura = factura;
                oListaDeFacturas.DetalleFacturaProducto = facturaDetalle;

                return oListaDeFacturas;
            }
        }

    }
}
