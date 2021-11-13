using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Linq;

namespace Aplication.Especifications
{
    public class SearchAllFacturasAndByCodigo : Specification<Factura>
    {
        public SearchAllFacturasAndByCodigo(Guid codigo, int pageSize, int pageNumber)
        {
            if (codigo.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                if (!string.IsNullOrEmpty(codigo.ToString()))
                {
                    //Buscamos tipo like por el nombre ingresado
                    Query.Search(x => x.Codigo.ToString(), "%" + codigo + "%");
                }

            }

            //Paginación . Datos segmentados por las paginas. que se van obteniendo.
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


        }
    }
}
