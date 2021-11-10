using Domain.Common;
using System;

namespace Domain.Entities
{
    public class CategoriaProducto : AuditableBaseEntity
    {
        public Guid CategoriaProductoId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; } = default!;


    }
}
