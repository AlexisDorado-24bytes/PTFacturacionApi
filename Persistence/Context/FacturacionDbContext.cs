using Aplication.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Context
{
    internal class FacturacionDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;

        public FacturacionDbContext(DbContextOptions<FacturacionDbContext> options, IDateTimeService dateTime) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        public DbSet<CategoriaProducto> CategoriaProducto { get; set; }

        //Se modifica en guardar los cambios para que también agrege la ultima fecha de modificacion y la de creación.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //Como todas las entidades heredan de AuditableBaseEntity, se verán afectadas
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    //Seteamos la fecha de modificación
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;

                        break;
                    //Seteamos la fecha de creación
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
