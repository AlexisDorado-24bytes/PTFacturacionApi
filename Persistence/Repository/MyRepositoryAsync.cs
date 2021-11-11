using Aplication.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
    //Implementacion de patron repositorio
    internal class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly FacturacionDbContext dbContext;

        public MyRepositoryAsync(FacturacionDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
