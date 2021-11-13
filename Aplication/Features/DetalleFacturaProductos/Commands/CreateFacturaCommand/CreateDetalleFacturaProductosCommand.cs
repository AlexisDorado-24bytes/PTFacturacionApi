using Aplication.Especifications;
using Aplication.Exceptions;
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
        private decimal ValorUnitario { get; set; }
        private decimal ValorTotal { get; set; }

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
                //Verificamos existencia del producto
                await ExtisteProducto(request.ProductoId);

                //Verificamos existencia de la factura con ek código único
                await ExtisteFactura(request.Codigo);

                //Buscamos el producto
                var producto = await _repositoryAsyncProducto.GetByIdAsync(request.ProductoId);

                //Validamos existencia de stock
                var existenciaSuficiente = ValidadExistenciaEstock(producto, request.Cantidad);

                if (!existenciaSuficiente)
                {

                    throw new ApiException($"El producto {producto.Nombre} no tiene el suficiente estock. Stock disponible: {producto.Stock}, cantidad solicitada es { request.Cantidad }.");
                }

                //Validamos si el producto ya existe en esa factura, no se crea nuevo registro, se actualiza cantidad y precio
                //Si existe ya un producto asociado a esa factura, se actualiza
                var existeEnBd = await VerificarActualizarRegistroVenta(request.Codigo, producto, request.Cantidad);

                //Todo el proceso para crear un registro y actualizar productos y factura
                if (!existeEnBd)
                {
                    var nuevoRegistro = _mapper.Map<DetalleFacturaProducto>(request);

                    //Actualizamos dara
                    nuevoRegistro.ValorTotal = producto.PrecioVentaCliente * request.Cantidad;
                    nuevoRegistro.ValorUnitario = producto.PrecioVentaCliente;


                    //Creamos el nuevo registro
                    var data = await _repositoryAsync.AddAsync(nuevoRegistro);

                    //Actualizamos el estock en la bd
                    producto.Stock = producto.Stock - request.Cantidad;

                    await _repositoryAsyncProducto.UpdateAsync(producto);



                    //Una vez se creado y guardado, pasamos a actualizar el valor total de la factura y los productos
                    await ActualizarTotalFactutaYCantidad(request.Codigo, nuevoRegistro.ValorTotal, request.Cantidad);

                    return new Response<Guid>(data.DetalleFacturaProductoId);
                }
                else
                {
                    return new Response<Guid>(request.Codigo);

                }

            }

            public bool ValidadExistenciaEstock(Producto producto, int cantidad)
            {
                if (producto.Stock < cantidad)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            public async Task<List<Factura>> ActualizarTotalFactutaYCantidad(Guid codigoFactura, decimal valorTotal, int cantidad)
            {
                var factura = await _repositoryAsyncFactura.ListAsync(new CustomSearchesFacturas(codigoFactura));
                factura[0].ValorTotalVenta = factura[0].ValorTotalVenta + valorTotal;
                factura[0].TotalProductosVenta = factura[0].TotalProductosVenta + cantidad;

                await _repositoryAsyncFactura.UpdateAsync(factura[0]);

                return factura;

            }
            public async Task ExtisteFactura(Guid codido)
            {

                var producto = await _repositoryAsyncFactura.ListAsync(new CustomSearchesFacturas(codido));

                if (producto == null)
                {
                    throw new KeyNotFoundException($"No existe Factura con el codigo {codido} proporconado.");
                }
            }
            public async Task ExtisteProducto(Guid productoId)
            {

                var producto = await _repositoryAsyncProducto.GetByIdAsync(productoId);

                if (producto == null)
                {
                    throw new KeyNotFoundException($"No existe producto con el id {productoId} proporconado.");
                }
            }

            public async Task<bool> VerificarActualizarRegistroVenta(Guid codigoFactura, Producto producto, int cantidad)
            {
                var valorTotal = producto.PrecioVentaCliente * cantidad;

                //Buscamos todos los movimientos asociados al codigo de la factura
                var listaMovimientos = await _repositoryAsync.ListAsync(new SearchAllFacturasDetalleAndByCodigo(codigoFactura));

                if (listaMovimientos.Count > 1)
                {
                    //Iniciamos el contador para saber si dentro del forach hubieron coincidencias y se actuali´zaron los regostros
                    var contador = 0;
                    foreach (var movimiento in listaMovimientos)
                    {
                        if (movimiento.ProductoId == producto.ProductoId)
                        {
                            //Suammos en 1 cada que se encuentra una coincidencia
                            contador++;
                            movimiento.Cantidad = movimiento.Cantidad + cantidad;
                            movimiento.ValorTotal = movimiento.ValorTotal + (producto.PrecioVentaCliente * cantidad);

                            //Actualizamos los registros de venta
                            await _repositoryAsync.UpdateAsync(movimiento);
                        }

                    }

                    //Al finalizar la iteración veriicamos si hubieron coincicencias y actualizamos los datos de la factura y producto
                    if (contador > 0)
                    {
                        //Pasamos a actualizar el valor total de la factura y cantidad de productos
                        await ActualizarTotalFactutaYCantidad(codigoFactura, valorTotal, cantidad);

                        //Actualizamos el estock en la bd
                        producto.Stock = producto.Stock - cantidad;
                        await _repositoryAsyncProducto.UpdateAsync(producto);

                        return true;
                    }
                }
                //Si solo se encuentra un movimiento en la bd
                else if (listaMovimientos.Count == 1)
                {
                    if (listaMovimientos[0].ProductoId == producto.ProductoId)
                    {
                        listaMovimientos[0].Cantidad = listaMovimientos[0].Cantidad + cantidad;
                        listaMovimientos[0].ValorTotal = listaMovimientos[0].ValorTotal + (producto.PrecioVentaCliente * cantidad);

                        //Actualizamos el regstroen la base de datos con los nuevos valores
                        await _repositoryAsync.UpdateAsync(listaMovimientos[0]);


                        //Actualizamos el estock en la bd
                        producto.Stock = producto.Stock - cantidad;
                        await _repositoryAsyncProducto.UpdateAsync(producto);

                        //Pasamos a actualizar el valor total de la factura y cantidad de productos
                        await ActualizarTotalFactutaYCantidad(codigoFactura, valorTotal, cantidad);

                        return true;
                    }

                }

                //Al no encntrar coincidencias, retornamos false
                return false;
            }

        }
    }

}
