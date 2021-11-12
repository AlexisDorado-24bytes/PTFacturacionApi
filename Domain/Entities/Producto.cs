using System;

namespace Domain.Entities
{
    public class Producto
    {
        public Guid ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public int Stock { get; set; }
        public decimal PrecioVentaCliente { get; set; }
        public CategoriaProducto CategoriaProducto { get; set; }
        public Guid CategoriaProductoId { get; set; }

    }
}
