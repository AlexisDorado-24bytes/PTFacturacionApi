using Ardalis.Specification;
using Domain.Entities;
using System;

namespace Aplication.Features.Facturas.Queries.CustomSearches
{
    public class CustomSearchesFacturas : Specification<Factura>
    {
        public CustomSearchesFacturas(Guid codigo)
        {
            if (!string.IsNullOrEmpty(codigo.ToString()))
            {
                //Buscamos tipo like por el nombre ingresado
                Query.Search(x => x.Codigo.ToString(), "%" + codigo + "%");
            }
        }
    }
}
