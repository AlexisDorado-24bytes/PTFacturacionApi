using Aplication.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication.Features.Categorias.Commands.CreateCategoriaCommand
{
    public class CreateCategoriaCommand : IRequest<Response<int>>
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; } = default!;
    }

    public class CreateCategoriaCommandHandler : IRequestHandler<CreateCategoriaCommand, Response<int>>
    {
        public async Task<Response<int>> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
