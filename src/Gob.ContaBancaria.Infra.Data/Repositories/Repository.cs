using System.Data;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gob.ContaBancaria.Infra.Data.Repositories
{
    public abstract class Repository<T> where T : Entity
    {
        protected readonly ContaBancariaDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(ContaBancariaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        protected IDbConnection GetDbConnection() => _context.Database.GetDbConnection();
    }
}
