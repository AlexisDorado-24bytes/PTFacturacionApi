using System;

namespace Aplication.DTOs
{
    public class FacturaDto
    {
        public DateTime FechaFactura { get; set; }
        public Guid Codigo { get; set; }
        public Guid ClienteId { get; set; }
        public int TotalProductosVenta { get; set; }
        public decimal ValorTotalVenta { get; set; }
    }
}
