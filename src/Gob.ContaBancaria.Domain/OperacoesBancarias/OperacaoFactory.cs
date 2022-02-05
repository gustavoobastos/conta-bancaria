using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.Options;
using Microsoft.Extensions.Options;

namespace Gob.ContaBancaria.Domain.OperacoesBancarias
{
    public class OperacaoFactory : IOperacaoFactory
    {
        public TaxasOperacionaisOptions TaxasOperacionais { get; }

        public OperacaoFactory(IOptions<TaxasOperacionaisOptions> taxasOperacionaisOptions)
        {
            TaxasOperacionais = taxasOperacionaisOptions.Value;
        }

        public Operacao CriarSaque(decimal valorOperacao, int idConta)
        {
            return new Saque(valorOperacao, TaxasOperacionais.TaxaSaque, idConta);
        }

        public Operacao CriarDeposito(decimal valorOperacao, int idConta)
        {
            return new Deposito(valorOperacao, TaxasOperacionais.TaxaDeposito, idConta);
        }

        public Operacao CriarTransferencia(decimal valorOperacao, int idConta, int idContaDestino)
        {
            return new Transferencia(valorOperacao, TaxasOperacionais.TaxaTransferencia, idConta, idContaDestino);
        }
    }
}
