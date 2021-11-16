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

namespace Aplication.Features.Clientes.Queries.GetAllClientesQueries
{
    public class GetAllClientes : IRequest<PagedResponse<List<ClienteDto>>>
    {
        public int CedulaCuidadania { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public class GetAllClientesHandler : IRequestHandler<GetAllClientes, PagedResponse<List<ClienteDto>>>
        {
            private readonly IRepositoryAsync<Cliente> _repositoryAsync;
            private readonly IMapper _mapper;


            public GetAllClientesHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<PagedResponse<List<ClienteDto>>> Handle(GetAllClientes request, CancellationToken cancellationToken)
            {
                var cliente = await _repositoryAsync.ListAsync(new SearchAllClientesByCedula(
                    request.CedulaCuidadania,
                    request.PageNumber,
                    request.PageSize
                    ));

                var dto = _mapper.Map<List<ClienteDto>>(cliente);

                return new PagedResponse<List<ClienteDto>>(dto, request.PageNumber, request.PageSize);

            }
        }
    }
}
