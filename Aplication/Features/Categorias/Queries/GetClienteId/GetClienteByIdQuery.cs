using Aplication.DTOs;
using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Categorias.Queries.GetClienteId
{
    public class GetClienteByIdQuery : IRequest<Response<CategoriaProductoDto>>
    {
        public Guid CategoriaProductoId { get; set; }

    }

    public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, Response<CategoriaProductoDto>>
    {
        private readonly IRepositoryAsync<CategoriaProducto> _repositoryAsync;
        private readonly IMapper _mapper;


        public GetClienteByIdHandler(IRepositoryAsync<CategoriaProducto> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<CategoriaProductoDto>> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var categoriaProducto = await _repositoryAsync.GetByIdAsync(request.CategoriaProductoId);
            if (categoriaProducto == null)
            {
                throw new KeyNotFoundException($"Categoría no encontrada con el id {request.CategoriaProductoId} proporconado.");
            }
            else
            {
                var dto = _mapper.Map<CategoriaProductoDto>(categoriaProducto);
                return new Response<CategoriaProductoDto>(dto);
            }
        }
    }

}
