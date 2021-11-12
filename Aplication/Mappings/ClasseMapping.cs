using Aplication.DTOs;
using Aplication.Features.Categorias.Commands.CreateCategoriaCommand;
using Aplication.Features.DetalleFacturaProductos.Commands.CreateFacturaCommand;
using Aplication.Features.Facturas.Commands.CreateFacturaCommand;
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

            #endregion

            #region Commands
            CreateMap<CreateDetalleFacturaProductosCommand, DetalleFacturaProducto>();
            CreateMap<CreateCategoriaCommand, CategoriaProducto>();
            CreateMap<CreateFacturaCommand, Factura>();
            #endregion
        }
    }
}
