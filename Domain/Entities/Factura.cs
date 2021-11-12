using System;

namespace Domain.Entities
{
    public class Factura
    {
        public Guid FacturaId { get; set; }
        public DateTime FechaFactura { get; set; }
        public Guid Codigo { get; set; }
        public Cliente Cliente { get; set; }
        public decimal ValorTotalVenta { get; set; }

    }
}
