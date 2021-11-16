using Aplication.DTOs;
using Aplication.Especifications;
using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Productos.Queries.GetAllProductosCommands
{
    public class GetAllProductosCommands : IRequest<PagedResponse<List<ProductoDto>>>
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }


        public class GetAllProductosCommandsHandler : IRequestHandler<GetAllProductosCommands, PagedResponse<List<ProductoDto>>>
        {
            private readonly IRepositoryAsync<Producto> _repositoryAsync;
            private readonly IMapper _mapper;


            public GetAllProductosCommandsHandler(IRepositoryAsync<Producto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PagedResponse<List<ProductoDto>>> Handle(GetAllProductosCommands request, CancellationToken cancellationToken)
            {
                //Arroja un listado de productos de acuerdo a las especificaciones en la clase GetAllAndFilterNombre
                var productos = await _repositoryAsync.ListAsync(new GetAllProductosAndFilterNombre(
                    request.Nombre,
                    request.PageNumber,
                    request.PageSize));

                //Mapeamos la lista obtenida a CategoriaProductoDto.
                var productosDto = _mapper.Map<List<ProductoDto>>(productos);

                //Retornamos la lista paginada y filtrada con el patron Especification
                return new PagedResponse<List<ProductoDto>>(productosDto, request.PageNumber, request.PageSize);
            }
        }
    }

}