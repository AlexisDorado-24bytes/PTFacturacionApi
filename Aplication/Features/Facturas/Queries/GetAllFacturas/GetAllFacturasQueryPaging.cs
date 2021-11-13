using Aplication.DTOs;
using Aplication.Especifications;
using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Facturas.Queries.GetAllFacturas
{
    public class GetAllFacturasQueryPaging : IRequest<PagedResponse<List<FacturaDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid Codigo { get; set; }


        public class GetAllFacturasQueryPagingHandler : IRequestHandler<GetAllFacturasQueryPaging, PagedResponse<List<FacturaDto>>>
        {
            private readonly IRepositoryAsync<Factura> _repositoryAsync;
            private readonly IMapper _mapper;


            public GetAllFacturasQueryPagingHandler(IRepositoryAsync<Factura> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PagedResponse<List<FacturaDto>>> Handle(GetAllFacturasQueryPaging request, CancellationToken cancellationToken)
            {
                var facturas = await _repositoryAsync.ListAsync(new SearchAllFacturasAndByCodigo(
                    request.Codigo,
                    request.PageSize,
                    request.PageNumber));

                var dto = _mapper.Map<List<FacturaDto>>(facturas);

                return new PagedResponse<List<FacturaDto>>(dto, request.PageNumber, request.PageSize);

            }
        }
    }
}
