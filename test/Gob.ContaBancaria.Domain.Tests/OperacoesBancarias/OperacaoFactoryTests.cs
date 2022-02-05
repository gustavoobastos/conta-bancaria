using Gob.ContaBancaria.Domain.OperacoesBancarias;
using Gob.ContaBancaria.Domain.Tests.Helpers;
using Xunit;

namespace Gob.ContaBancaria.Domain.Tests.OperacoesBancarias
{
    public class OperacaoFactoryTests
    {
        private readonly OperacaoFactory _operacaoFactory;

        public OperacaoFactoryTests()
        {
            _operacaoFactory = OperacaoFactoryHelper.CriarOperacaoFactoryTest();
        }

        [Fact]
        public void OperacaoFactory_CriarSaque_Sucesso()
        {
            // Arrange
            decimal valorOperacao = 200.00m;
            decimal valorTotalOperacao = 204.00m;
            int idConta = 1;

            // Act
            Operacao operacao = _operacaoFactory.CriarSaque(valorOperacao, idConta);

            // Assert
            Assert.IsType<Saque>(operacao);
            Assert.Equal(valorOperacao, operacao.ValorOperacao);
            Assert.Equal(valorTotalOperacao, operacao.ValorTotalOperacao);
            Assert.Equal(_operacaoFactory.TaxasOperacionais.TaxaSaque, operacao.ValorTaxa);
            Assert.Equal(2, operacao.Lacamentos.Count);
        }

        [Fact]
        public void OperacaoFactory_CriarTransferencia_Sucesso()
        {
            // Arrange
            decimal valorOperacao = 200.00m;
            decimal valorTotalOperacao = 201.00m;
            int idConta = 1;
            int idContaDestino = 2;

            // Act
            Operacao operacao = _operacaoFactory.CriarTransferencia(valorOperacao, idConta, idContaDestino);

            // Assert
            Assert.IsType<Transferencia>(operacao);
            Assert.Equal(valorOperacao, operacao.ValorOperacao);
            Assert.Equal(valorTotalOperacao, operacao.ValorTotalOperacao);
            Assert.Equal(_operacaoFactory.TaxasOperacionais.TaxaTransferencia, operacao.ValorTaxa);
            Assert.Equal(3, operacao.Lacamentos.Count);
        }

        [Theory]
        [InlineData(2, 2.00, 198.00, 200.00)]
        [InlineData(1, 0.00, 0.05, 0.05)]
        public void OperacaoFactory_CriarDeposito_Sucesso(int expectedLancamentos, decimal expectedTaxa, decimal valorTotalOperacao, decimal valorOperacao)
        {
            // Arrange
            int idConta = 1;

            // Act
            Operacao operacao = _operacaoFactory.CriarDeposito(valorOperacao, idConta);

            // Assert
            Assert.IsType<Deposito>(operacao);
            Assert.Equal(valorOperacao, operacao.ValorOperacao);
            Assert.Equal(valorTotalOperacao, operacao.ValorTotalOperacao);
            Assert.Equal(expectedTaxa, operacao.ValorTaxa);
            Assert.Equal(expectedLancamentos, operacao.Lacamentos.Count);
        }
    }
}
