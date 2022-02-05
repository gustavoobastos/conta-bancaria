using System.Collections.Generic;
using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Domain.ViewModels;

namespace Gob.ContaBancaria.Domain.Interfaces
{
    public interface ILancamentoRepository
    {
        Task<int> SalvarLancamentosAsync(IEnumerable<Lancamento> lancamentos);
        Task<IEnumerable<ExtratoViewModel>> BuscarExtratoAsync(int idConta);
    }
}
