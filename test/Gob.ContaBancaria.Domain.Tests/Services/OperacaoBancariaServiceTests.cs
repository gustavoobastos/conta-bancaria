using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.OperacoesBancarias;
using Gob.ContaBancaria.Domain.Requests;
using Gob.ContaBancaria.Domain.Services;
using Gob.ContaBancaria.Domain.Tests.Helpers;
using Gob.ContaBancaria.Domain.ViewModels;
using Moq;
using Xunit;

namespace Gob.ContaBancaria.Domain.Tests.Services
{
    public class OperacaoBancariaServiceTests
    {
        private readonly OperacaoFactory _operacaoFactory = OperacaoFactoryHelper.CriarOperacaoFactoryTest();
        private readonly Mock<IContaRepository> _mockContaRepository = new();
        private readonly Mock<ILancamentoRepository> _mockLancamentoRepository = new();

        [Fact]
        public async Task OperacaoBancariaService_DepositarAsync_Sucesso()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(It.IsAny<int>())).ReturnsAsync(true);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            DepositoRequest request = new() { IdConta = 1, Valor = 100m };

            // Act
            BaseResult result = await service.DepositarAsync(request);

            // Assert
            Assert.True(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_DepositarAsync_ContaNaoExiste()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(It.IsAny<int>())).ReturnsAsync(false);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            DepositoRequest request = new() { IdConta = 1, Valor = 100m };

            // Act
            BaseResult result = await service.DepositarAsync(request);

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_SacarAsync_Sucesso()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(It.IsAny<int>())).ReturnsAsync(true);
            _mockContaRepository.Setup(x => x.BuscarSaldoContaAsync(It.IsAny<int>())).ReturnsAsync(1000m);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            SaqueRequest request = new() { IdConta = 1, Valor = 100m };

            // Act
            BaseResult result = await service.SacarAsync(request);

            // Assert
            Assert.True(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_SacarAsync_ContaNaoExiste()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(It.IsAny<int>())).ReturnsAsync(false);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            SaqueRequest request = new() { IdConta = 1, Valor = 100m };

            // Act
            BaseResult result = await service.SacarAsync(request);

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_SacarAsync_SaldoInsuficiente()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(It.IsAny<int>())).ReturnsAsync(true);
            _mockContaRepository.Setup(x => x.BuscarSaldoContaAsync(It.IsAny<int>())).ReturnsAsync(0m);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            SaqueRequest request = new() { IdConta = 1, Valor = 100m };

            // Act
            BaseResult result = await service.SacarAsync(request);

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_TransferirAsync_Sucesso()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(It.IsAny<int>())).ReturnsAsync(true);
            _mockContaRepository.Setup(x => x.BuscarSaldoContaAsync(It.IsAny<int>())).ReturnsAsync(1000m);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            TransferenciaRequest request = new() { IdContaOrigem = 1, IdContaDestino = 2, Valor = 100m };

            // Act
            BaseResult result = await service.TransferirAsync(request);

            // Assert
            Assert.True(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_TransferirAsync_TransferenciaParaMesmaConta()
        {
            // Arrange
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            TransferenciaRequest request = new() { IdContaOrigem = 1, IdContaDestino = 1, Valor = 100m };

            // Act
            BaseResult result = await service.TransferirAsync(request);

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_TransferirAsync_ContaOrigemNaoExiste()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(1)).ReturnsAsync(false);
            _mockContaRepository.Setup(x => x.ContaExisteAsync(2)).ReturnsAsync(true);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            TransferenciaRequest request = new() { IdContaOrigem = 1, IdContaDestino = 2, Valor = 100m };

            // Act
            BaseResult result = await service.TransferirAsync(request);

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_TransferirAsync_ContaDestinoNaoExiste()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(1)).ReturnsAsync(true);
            _mockContaRepository.Setup(x => x.ContaExisteAsync(2)).ReturnsAsync(false);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            TransferenciaRequest request = new() { IdContaOrigem = 1, IdContaDestino = 2, Valor = 100m };

            // Act
            BaseResult result = await service.TransferirAsync(request);

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task OperacaoBancariaService_TransferirAsync_SaldoInsuficiente()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.ContaExisteAsync(It.IsAny<int>())).ReturnsAsync(true);
            _mockContaRepository.Setup(x => x.BuscarSaldoContaAsync(It.IsAny<int>())).ReturnsAsync(0m);
            OperacaoBancariaService service = new(_mockContaRepository.Object, _mockLancamentoRepository.Object, _operacaoFactory);
            TransferenciaRequest request = new() { IdContaOrigem = 1, IdContaDestino = 2, Valor = 100m };

            // Act
            BaseResult result = await service.TransferirAsync(request);

            // Assert
            Assert.False(result.Sucesso);
        }
    }
}
