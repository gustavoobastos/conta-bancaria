using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Models;

namespace Gob.ContaBancaria.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task<Pessoa?> BuscarPessoaAsync(string cpf);
    }
}
