using Gob.ContaBancaria.Domain.OperacoesBancarias;

namespace Gob.ContaBancaria.Domain.Interfaces
{
    public interface IOperacaoFactory
    {
        Operacao CriarDeposito(decimal valorOperacao, int idConta);
        Operacao CriarSaque(decimal valorOperacao, int idConta);
        Operacao CriarTransferencia(decimal valorOperacao, int idConta, int idContaDestino);
    }
}
