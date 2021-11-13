using Domain.Entities;
using System.Collections.Generic;

namespace Aplication.Wrappers
{
    public class ListaDeFacturas
    {
        public List<Factura> Factura { get; set; }
        public List<DetalleFacturaProducto> DetalleFacturaProducto { get; set; }
    }
}
