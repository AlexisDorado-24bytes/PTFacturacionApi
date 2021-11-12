using Ardalis.Specification;
using Domain.Entities;

//IMplementamos el Specification Pattern
namespace Aplication.Especifications
{
    public class PagedCategoriaEspecification : Specification<CategoriaProducto>
    {
        public PagedCategoriaEspecification(int pageSize, int pageNumber, string nombre, string codigo)
        {
            //Paginación . Datos segmentados por las paginas. que se van obteniendo.
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))
            {
                //Buscamos tipo like por el nombre ingresado
                Query.Search(x => x.Nombre, "%" + nombre + "%");
            }
            if (!string.IsNullOrEmpty(codigo))
            {
                //Buscamos tipo like por el codigo ingresado
                Query.Search(x => x.Codigo, "%" + codigo + "%");
            }
        }
    }
}
