using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Domain.Requests;
using Gob.ContaBancaria.Domain.Services;
using Gob.ContaBancaria.Domain.ViewModels;
using Moq;
using Xunit;

namespace Gob.ContaBancaria.Domain.Tests.Services
{
    public class ContaBancariaServiceTests
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IPessoaRepository> _mockPessoaRepository = new();
        private readonly Mock<IContaRepository> _mockContaRepository = new();
        private readonly Mock<ILancamentoRepository> _mockLacamentoRepository = new();

        [Theory]
        [MemberData(nameof(BuscarPessoaAsync))]
        public async Task ContaBancariaService_CriarContaAsync_Sucesso(Pessoa? pessoa)
        {
            // Arrange
            _mockPessoaRepository.Setup(x => x.BuscarPessoaAsync(It.IsAny<string>())).ReturnsAsync(pessoa);
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);
            CriarContaBancariaRequest request = new() { CpfTitular = "11111111111", NomeTitular = "Gustavo Bastos" };

            // Act
            BaseResult result = await service.CriarContaAsync(request);

            // Assert
            Assert.True(result.Sucesso);
        }

        public static IEnumerable<object[]> BuscarPessoaAsync()
        {
            yield return new object[] { new Pessoa("11111111111", "Gustavo Bastos") };
            yield return new object[] { null };
        }

        [Fact]
        public async Task ContaBancariaService_BuscarContaAsync_Sucesso()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.BuscarContaAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<ContaViewModel>());
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);

            // Act
            BaseResult result = await service.BuscarContaAsync(1);

            // Assert
            Assert.True(result.Sucesso);
        }

        [Fact]
        public async Task ContaBancariaService_BuscarContaAsync_ContaNaoExiste()
        {
            // Arrange
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);

            // Act
            BaseResult result = await service.BuscarContaAsync(1);

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task ContaBancariaService_BuscarContasAsync_Sucesso()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.BuscarContasAsync()).ReturnsAsync(_fixture.CreateMany<ContaViewModel>());
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);

            // Act
            BaseResult result = await service.BuscarContasAsync();

            // Assert
            Assert.True(result.Sucesso);
        }

        [Fact]
        public async Task ContaBancariaService_BuscarContasAsync_ContaNaoExiste()
        {
            // Arrange
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);

            // Act
            BaseResult result = await service.BuscarContasAsync();

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task ContaBancariaService_SaldoAsync_Sucesso()
        {
            // Arrange
            _mockContaRepository.Setup(x => x.BuscarSaldoContaAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<decimal>());
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);

            // Act
            BaseResult result = await service.SaldoAsync(1);

            // Assert
            Assert.True(result.Sucesso);
        }

        [Fact]
        public async Task ContaBancariaService_SaldoAsync_ContaNaoExiste()
        {
            // Arrange
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);

            // Act
            BaseResult result = await service.SaldoAsync(1);

            // Assert
            Assert.False(result.Sucesso);
        }

        [Fact]
        public async Task ContaBancariaService_ExtratoAsync_Sucesso()
        {
            // Arrange
            _mockLacamentoRepository.Setup(x => x.BuscarExtratoAsync(It.IsAny<int>())).ReturnsAsync(_fixture.CreateMany<ExtratoViewModel>());
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);

            // Act
            BaseResult result = await service.ExtratoAsync(1);

            // Assert
            Assert.True(result.Sucesso);
        }

        [Fact]
        public async Task ContaBancariaService_ExtratoAsync_ContaNaoExiste()
        {
            // Arrange
            ContaBancariaService service = new(_mockPessoaRepository.Object, _mockContaRepository.Object, _mockLacamentoRepository.Object);

            // Act
            BaseResult result = await service.ExtratoAsync(1);

            // Assert
            Assert.False(result.Sucesso);
        }
    }
}
