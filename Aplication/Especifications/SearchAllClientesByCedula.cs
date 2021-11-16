using Ardalis.Specification;
using Domain.Entities;

namespace Aplication.Especifications
{
    public class SearchAllClientesByCedula : Specification<Cliente>
    {
        public SearchAllClientesByCedula(int cedula, int pageNumber, int pageSize)
        {
            //Paginación . Datos segmentados por las paginas. que se van obteniendo.
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


            if (!string.IsNullOrEmpty(cedula.ToString()))
            {
                //Buscamos tipo like por el nombre ingresado
                Query.Search(x => x.CedulaCliente.ToString(), "%" + cedula + "%");
            }

        }
    }
}
