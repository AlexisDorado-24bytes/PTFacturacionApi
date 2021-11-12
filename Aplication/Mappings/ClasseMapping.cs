using Aplication.DTOs;
using Aplication.Features.Categorias.Commands.CreateCategoriaCommand;
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
            CreateMap<CreateCategoriaCommand, CategoriaProducto>();
            #endregion
        }
    }
}
