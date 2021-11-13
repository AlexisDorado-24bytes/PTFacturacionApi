using Aplication.DTOs;
using Aplication.Features.Categorias.Commands.CreateCategoriaCommand;
using Aplication.Features.DetalleFacturaProductos.Commands.CreateFacturaCommand;
using Aplication.Features.Facturas.Commands.CreateFacturaCommand;
using Aplication.Features.Productos.Commands.CreateProductosCommand;
using AutoMapper;
using Domain.Entities;

namespace Aplication.Mappings
{
    internal class ClasseMapping : Profile
    {
        public ClasseMapping()
        {
            #region DTOs
            CreateMap<CategoriaProductoDto, CategoriaProducto>().ReverseMap();
            CreateMap<FacturaDto, Factura>().ReverseMap();
            CreateMap<ProductoDto, Producto>().ReverseMap();

            #endregion

            #region Commands
            CreateMap<CreatedProductoCommand, Producto>();
            CreateMap<CreateDetalleFacturaProductosCommand, DetalleFacturaProducto>().ReverseMap();
            CreateMap<CreateCategoriaCommand, CategoriaProducto>();
            CreateMap<CreateFacturaCommand, Factura>();
            #endregion
        }
    }
}
