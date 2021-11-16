using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Facturas.Commands.CreateFacturaCommand
{
    public class CreateFacturaCommand : IRequest<Response<Guid>>
    {
        public DateTime FechaFactura { get; set; }
        public Guid Codigo { get; set; }
        public Guid ClienteId { get; set; }

        public class CreateFacturaCommandHandler : IRequestHandler<CreateFacturaCommand, Response<Guid>>
        {
            private readonly IRepositoryAsync<Factura> _repositoryAsync;
            private readonly IRepositoryAsync<Cliente> _repositoryAsyncCliente;
            private readonly IMapper _mapper;

            public CreateFacturaCommandHandler(IRepositoryAsync<Cliente> repositoryAsyncCliente, IRepositoryAsync<Factura> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _repositoryAsyncCliente = repositoryAsyncCliente;
                _mapper = mapper;
            }

            public async Task<Response<Guid>> Handle(CreateFacturaCommand request, CancellationToken cancellationToken)
            {

                var nuevoRegistro = _mapper.Map<Factura>(request);

                var data = await _repositoryAsync.AddAsync(nuevoRegistro);
                return new Response<Guid>(data.FacturaId);
            }
        }
    }
}
