using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Categorias.Commands.EditCategoriaCommand
{
    public class EditCategoriaCommand : IRequest<Response<Guid>>
    {
        public Guid CategoriaProductoId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; } = default!;

        public class EditCategoriaCommandHandler : IRequestHandler<EditCategoriaCommand, Response<Guid>>
        {
            private readonly IRepositoryAsync<CategoriaProducto> _repositoryAsync;
            private readonly IMapper _mapper;


            public EditCategoriaCommandHandler(IRepositoryAsync<CategoriaProducto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }


            public async Task<Response<Guid>> Handle(EditCategoriaCommand request, CancellationToken cancellationToken)
            {
                var categoriaProducto = await _repositoryAsync.GetByIdAsync(request.CategoriaProductoId);
                if (categoriaProducto == null)
                {
                    throw new KeyNotFoundException($"Categoría no encontrada con el id {request.CategoriaProductoId} proporconado.");
                }
                else
                {
                    categoriaProducto.Nombre = request.Nombre;
                    categoriaProducto.Codigo = request.Codigo;
                    categoriaProducto.Descripcion = request.Descripcion;

                    await _repositoryAsync.UpdateAsync(categoriaProducto);

                    return new Response<Guid>(categoriaProducto.CategoriaProductoId);
                }

            }
        }
    }
}
