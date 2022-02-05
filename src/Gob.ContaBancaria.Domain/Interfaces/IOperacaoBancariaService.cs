using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Requests;
using Gob.ContaBancaria.Domain.ViewModels;

namespace Gob.ContaBancaria.Domain.Interfaces
{
    public interface IOperacaoBancariaService
    {
        Task<BaseResult> DepositarAsync(DepositoRequest request);
        Task<BaseResult> SacarAsync(SaqueRequest request);
        Task<BaseResult> TransferirAsync(TransferenciaRequest request);
    }
}
