using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ProductoConfig : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");
            builder.HasKey(p => p.ProductoId);
            builder.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Descripcion).HasMaxLength(300).IsRequired();
            builder.Property(p => p.PrecioCompra).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.PrecioVentaCliente).HasColumnType("decimal(18,2)").IsRequired();
        }

    }
}
