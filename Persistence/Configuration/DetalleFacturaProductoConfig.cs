using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class DetalleFacturaProductoConfig : IEntityTypeConfiguration<DetalleFacturaProducto>
    {

        public void Configure(EntityTypeBuilder<DetalleFacturaProducto> builder)
        {
            builder.ToTable("DetalleFacturaProductos");
            builder.HasKey(p => p.DetalleFacturaProductoId);

            builder.Property(p => p.Codigo).HasMaxLength(100).IsRequired();

            builder.Property(p => p.ValorUnitario).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.ValorTotal).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Cantidad).HasColumnType("int").IsRequired();
        }
    }
}
