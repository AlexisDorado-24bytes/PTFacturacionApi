using Aplication.Parameters;

namespace Aplication.Features.Categorias.Queries.GetAllClientes
{
    public class GetAllCategoriaParameters : RequestParameters
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
}
