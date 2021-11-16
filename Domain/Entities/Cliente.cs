using System;

namespace Domain.Entities
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public int CedulaCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
