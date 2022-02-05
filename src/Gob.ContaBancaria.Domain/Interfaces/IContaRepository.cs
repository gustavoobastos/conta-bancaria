using System.Collections.Generic;
using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Domain.ViewModels;

namespace Gob.ContaBancaria.Domain.Interfaces
{
    public interface IContaRepository
    {
        Task<decimal?> BuscarSaldoContaAsync(int idConta);
        Task<int> CriarContaAsync(Conta conta);
        Task<ContaViewModel?> BuscarContaAsync(int idConta);
        Task<IEnumerable<ContaViewModel>> BuscarContasAsync();
        Task<bool> ContaExisteAsync(int idConta);
    }
}
