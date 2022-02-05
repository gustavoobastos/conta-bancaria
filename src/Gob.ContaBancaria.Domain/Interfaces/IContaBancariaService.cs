using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Requests;
using Gob.ContaBancaria.Domain.ViewModels;

namespace Gob.ContaBancaria.Domain.Interfaces
{
    public interface IContaBancariaService
    {
        Task<BaseResult> CriarContaAsync(CriarContaBancariaRequest request);
        Task<BaseResult> BuscarContasAsync();
        Task<BaseResult> BuscarContaAsync(int idConta);
        Task<BaseResult> SaldoAsync(int idConta);
        Task<BaseResult> ExtratoAsync(int idConta);
    }
}
