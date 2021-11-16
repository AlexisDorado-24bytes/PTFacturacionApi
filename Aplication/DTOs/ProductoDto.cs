using System;

namespace Aplication.DTOs
{
    public class ProductoDto
    {
        public Guid ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal PrecioVentaCliente { get; set; }
        public Guid CategoriaProductoId { get; set; }
    }
}
