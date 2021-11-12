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

namespace Aplication.Features.Categorias.Queries.GetAllClientes
{
    public class GetAllCategoriesQueryPaging : IRequest<PagedResponse<List<CategoriaProductoDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }

        public class GetAllCategoriesQueryPagingHandler : IRequestHandler<GetAllCategoriesQueryPaging, PagedResponse<List<CategoriaProductoDto>>>
        {
            private readonly IRepositoryAsync<CategoriaProducto> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllCategoriesQueryPagingHandler(IRepositoryAsync<CategoriaProducto> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }


            public async Task<PagedResponse<List<CategoriaProductoDto>>> Handle(GetAllCategoriesQueryPaging request, CancellationToken cancellationToken)
            {
                //Arroja un listado de categorias de acuerdo a las especificaciones en la clase PagedCategoriaEspecification
                var categorias = await _repositoryAsync.ListAsync(new PagedCategoriaEspecification(
                    request.PageSize,
                    request.PageNumber,
                    request.Nombre,
                    request.Codigo)
                    );

                //Mapeamos la lista obtenida a CategoriaProductoDto.
                var categoriasDto = _mapper.Map<List<CategoriaProductoDto>>(categorias);

                //Retornamos la lista paginada y filtrada con el patron Especification
                return new PagedResponse<List<CategoriaProductoDto>>(categoriasDto, request.PageNumber, request.PageSize);
            }
        }
    }
}
