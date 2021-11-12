using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Categorias.Commands.DeleteCategoriaCommand
{
    public class DeleteCategoriaCommand : IRequest<Response<Guid>>
    {
        public Guid CategoriaProductoId { get; set; }
    }

    public class DeleteCategoriaCommandHandler : IRequestHandler<DeleteCategoriaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<CategoriaProducto> _repositoryAsync;
        private readonly IMapper _mapper;


        public DeleteCategoriaCommandHandler(IRepositoryAsync<CategoriaProducto> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoriaProducto = await _repositoryAsync.GetByIdAsync(request.CategoriaProductoId);
            if (categoriaProducto == null)
            {
                throw new KeyNotFoundException($"Categoría no encontrada con el id {request.CategoriaProductoId} proporconado.");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(categoriaProducto);

                return new Response<Guid>(categoriaProducto.CategoriaProductoId);
            }

        }
    }
}
