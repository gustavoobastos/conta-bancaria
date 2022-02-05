using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Extensions;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Domain.ViewModels;
using Gob.ContaBancaria.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gob.ContaBancaria.Infra.Data.Repositories
{
    public class LancamentoRepository : Repository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(ContaBancariaDbContext context) : base(context)
        {
        }

        public async Task<int> SalvarLancamentosAsync(IEnumerable<Lancamento> lancamentos)
        {
            _dbSet.AddRange(lancamentos);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExtratoViewModel>> BuscarExtratoAsync(int idConta)
        {
            List<ExtratoViewModel> extratos = (await _dbSet.AsNoTracking().Where(x => x.IdConta == idConta).ToListAsync())
                .OrderBy(x => x.Data).ThenBy(x => x.TipoLancamento)
                .Select(x => x.ToExtratoViewModel())
                .CalcularSaldo()
                .ToList();

            extratos.Reverse();

            return extratos;
        }
    }
}
