using Ardalis.Specification;
using Domain.Entities;

namespace Aplication.Especifications
{
    internal class GetAllProductosAndFilterNombre : Specification<Producto>
    {
        public GetAllProductosAndFilterNombre(string nombre, int pageNumber, int pageSize)
        {
            //Paginación . Datos segmentados por las paginas. que se van obteniendo.
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


            if (!string.IsNullOrEmpty(nombre))
            {
                //Buscamos tipo like por el nombre ingresado
                Query.Search(x => x.Nombre, "%" + nombre + "%");
            }

        }
    }
}

