using Aplication.Features.Categorias.Commands.CreateCategoriaCommand;
using AutoMapper;
using Domain.Entities;

namespace Aplication.Mappings
{
    internal class ClasseMapping : Profile
    {
        public ClasseMapping()
        {
            #region Commands
            CreateMap<CreateCategoriaCommand, CategoriaProducto>();
            #endregion
        }
    }
}
