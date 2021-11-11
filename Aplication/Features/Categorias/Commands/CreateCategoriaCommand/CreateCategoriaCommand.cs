using Aplication.Interfaces;
using Aplication.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Categorias.Commands.CreateCategoriaCommand
{
    public class CreateCategoriaCommand : IRequest<Response<Guid>>
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; } = default!;
    }

    public class CreateCategoriaCommandHandler : IRequestHandler<CreateCategoriaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<CategoriaProducto> _repositoryAsync;
        private readonly IMapper _mapper;


        public CreateCategoriaCommandHandler(IRepositoryAsync<CategoriaProducto> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }


        public async Task<Response<Guid>> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var nuevoRegistro = _mapper.Map<CategoriaProducto>(request);
            var data = await _repositoryAsync.AddAsync(nuevoRegistro);
            return new Response<Guid>(data.CategoriaProductoId);

        }
    }
}
