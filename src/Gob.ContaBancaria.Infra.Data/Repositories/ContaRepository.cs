using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Domain.ViewModels;
using Gob.ContaBancaria.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gob.ContaBancaria.Infra.Data.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(ContaBancariaDbContext context) : base(context)
        {
        }

        public Task<ContaViewModel?> BuscarContaAsync(int idConta)
        {
            return _dbSet
                .AsNoTracking()
                .Include(x => x.Titular)
                .Select(x => new ContaViewModel()
                {
                    IdConta = x.Id,
                    CpfTituar = x.Titular!.Cpf,
                    NomeTitular = x.Titular!.Nome
                })
                .FirstOrDefaultAsync(x => x.IdConta == idConta);
        }

        public async Task<IEnumerable<ContaViewModel>> BuscarContasAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Titular)
                .Select(x => new ContaViewModel()
                {
                    IdConta = x.Id,
                    CpfTituar = x.Titular!.Cpf,
                    NomeTitular = x.Titular!.Nome
                })
                .ToListAsync();
        }

        private static readonly string s_querySaldo = "SELECT (SELECT SUM(Valor) FROM Lancamento WHERE IdConta = @IdConta AND TipoLancamento = 0) - ISNULL((SELECT SUM(Valor) FROM Lancamento WHERE IdConta = @IdConta AND TipoLancamento = 1), 0) AS Saldo";
        public async Task<decimal?> BuscarSaldoContaAsync(int idConta)
        {
            return await GetDbConnection().ExecuteScalarAsync<decimal?>(s_querySaldo, new
            {
                IdConta = idConta,
            });
        }

        public Task<bool> ContaExisteAsync(int idConta)
        {
            return _dbSet.AsNoTracking().AnyAsync(x => x.Id == idConta);
        }

        public async Task<int> CriarContaAsync(Conta conta)
        {
            _dbSet.Add(conta);
            return await _context.SaveChangesAsync();
        }
    }
}
