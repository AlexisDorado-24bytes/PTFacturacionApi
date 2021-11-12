using System;

namespace Domain.Entities
{
    public class DetalleFacturaProducto
    {
        public Guid DetalleFacturaProductoId { get; set; }
        public string Codigo { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

    }
}
