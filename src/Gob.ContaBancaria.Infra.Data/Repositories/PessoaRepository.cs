using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gob.ContaBancaria.Infra.Data.Repositories
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(ContaBancariaDbContext context) : base(context)
        {
        }

        public Task<Pessoa?> BuscarPessoaAsync(string cpf)
        {
            return _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Cpf == cpf);
        }
    }
}
