using System;

namespace Aplication.DTOs
{
    public class CategoriaProductoDto
    {
        public Guid CategoriaProductoId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
