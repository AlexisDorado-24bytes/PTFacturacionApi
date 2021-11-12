using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class FacturaConfig : IEntityTypeConfiguration<Factura>
    {

        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder.ToTable("Facturas");
            builder.HasKey(p => p.FacturaId);
            builder.HasIndex(p => p.Codigo).IsUnique();

            builder.Property(p => p.ValorTotalVenta).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.FechaFactura).IsRequired();

        }
    }
}
