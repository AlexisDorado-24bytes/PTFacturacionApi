using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(p => p.ClienteId);
            builder.HasIndex(p => p.CedulaCliente).IsUnique();
            builder.Property(p => p.CedulaCliente).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Nombre).HasMaxLength(80).IsRequired();
            builder.Property(p => p.Apellido).HasMaxLength(200).IsRequired();
            builder.Property(p => p.FechaNacimiento).IsRequired();
            builder.Property(p => p.Telefono).HasMaxLength(15);
            builder.Property(p => p.Direccion).HasMaxLength(250);
        }

    }
}
