using System;

namespace Aplication.Features.Facturas.Queries.GetAllFacturas
{
    public class GetAllFacturasQueryPagingParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid Codigo { get; set; }
    }
}
