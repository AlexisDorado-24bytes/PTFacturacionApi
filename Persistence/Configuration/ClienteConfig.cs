using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ClienteConfig : IEntityTypeConfiguration<CategoriaProducto>
    {
        public void Configure(EntityTypeBuilder<CategoriaProducto> builder)
        {
            builder.ToTable("CategoriasProductos");
            builder.HasKey(p => p.CategoriaProductoId);
            builder.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Descripcion).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Codigo).HasMaxLength(50).IsRequired();


        }
    }
}
