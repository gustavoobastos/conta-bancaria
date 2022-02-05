using Gob.ContaBancaria.Domain.OperacoesBancarias;
using Gob.ContaBancaria.Domain.Options;
using Microsoft.Extensions.Options;

namespace Gob.ContaBancaria.Domain.Tests.Helpers
{
    internal static class OperacaoFactoryHelper
    {
        public static OperacaoFactory CriarOperacaoFactoryTest()
        {
            IOptions<TaxasOperacionaisOptions> options = Microsoft.Extensions.Options.Options.Create(new TaxasOperacionaisOptions()
            {
                TaxaDeposito = 0.01m,
                TaxaSaque = 4.00m,
                TaxaTransferencia = 1.00m
            });

            return new OperacaoFactory(options);
        }
    }
}
