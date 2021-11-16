using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Aplication.Especifications
{
    public class CustomSearchesFacturas : Specification<Factura>
    {
        public CustomSearchesFacturas(Guid codigo)
        {

            if (codigo.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                if (!string.IsNullOrEmpty(codigo.ToString()))
                {
                    //Buscamos tipo like por el nombre ingresado
                    Query.Search(x => x.Codigo.ToString(), "%" + codigo + "%");
                }

            }
            else
            {
                throw new KeyNotFoundException($"Debes enviar un código de factura.");

            }

        }
    }
}
