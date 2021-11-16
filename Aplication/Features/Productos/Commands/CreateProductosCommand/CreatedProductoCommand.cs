using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Productos.Commands.CreateProductosCommand
{
    public class CreatedProductoCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public int Stock { get; set; }
        public decimal PrecioVentaCliente { get; set; }
        public Guid CategoriaProductoId { get; set; }

        public class CreatedProductoCommandHandler : IRequestHandler<CreatedProductoCommand, Response<Guid>>
        {
            private readonly IRepositoryAsync<Producto> _repositoryAsyncProducto;
            private readonly IRepositoryAsync<CategoriaProducto> _repositoryAsyncCategoria;
            private readonly IMapper _mapper;

            public CreatedProductoCommandHandler(IRepositoryAsync<CategoriaProducto> repositoryAsyncCategoria, IRepositoryAsync<Producto> repositoryAsyncProducto, IMapper mapper)
            {
                _repositoryAsyncCategoria = repositoryAsyncCategoria;
                _mapper = mapper;
                _repositoryAsyncProducto = repositoryAsyncProducto;
            }

            public async Task<Response<Guid>> Handle(CreatedProductoCommand request, CancellationToken cancellationToken)
            {
                //validando existencia de categoria
                await ValidarCategoriaProducto(request.CategoriaProductoId);

                var nuevoRegistro = _mapper.Map<Producto>(request);

                var data = await _repositoryAsyncProducto.AddAsync(nuevoRegistro);

                return new Response<Guid>(data.ProductoId);

            }

            public async Task ValidarCategoriaProducto(Guid CategoriaId)
            {
                var categoria = await _repositoryAsyncCategoria.GetByIdAsync(CategoriaId);
                if (categoria == null)
                {
                    throw new KeyNotFoundException($"No existe Categoria con el id {CategoriaId} proporconado.");
                }

            }
        }
    }
}
