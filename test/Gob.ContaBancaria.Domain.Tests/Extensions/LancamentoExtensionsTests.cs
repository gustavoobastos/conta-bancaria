using System;
using System.Collections.Generic;
using System.ComponentModel;
using AutoFixture;
using Gob.ContaBancaria.Domain.Extensions;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Domain.ViewModels;
using Xunit;

namespace Gob.ContaBancaria.Domain.Tests.Extensions
{
    public class LancamentoExtensionsTests
    {
        [Theory]
        [InlineData(100.00, TipoLancamento.Entrada, 100.00)]
        [InlineData(-100.00, TipoLancamento.Saida, 100.00)]
        public void LancamentoExtensions_ToExtratoViewModel_Sucesso(decimal expectedValor, TipoLancamento tipoLancamento, decimal valor)
        {
            // Arrange
            Lancamento lancamento = new(1, null, Guid.NewGuid(), DateTime.UtcNow, tipoLancamento, TipoOperacao.Deposito, valor);

            // Act
            ExtratoViewModel viewModel = lancamento.ToExtratoViewModel();

            // Assert
            Assert.Equal(0, viewModel.Saldo);
            Assert.Equal(lancamento.Data.ToString("dd/MM/yyyy HH:mm:ss"), viewModel.Data);
            Assert.NotEmpty(viewModel.Operacao);
            Assert.Equal(expectedValor, viewModel.Valor);
        }

        [Theory]
        [MemberData(nameof(TipoOperacaoEnumValues))]
        public void LancamentoExtensions_ObterDescricaoOperacao_Sucesso(TipoOperacao tipoOperacao, TipoLancamento tipoLancamento)
        {
            // Arrange
            Lancamento lancamento = new(1, null, Guid.NewGuid(), DateTime.UtcNow, tipoLancamento, tipoOperacao, 100m);

            // Act
            string descricao = lancamento.ObterDescricaoOperacao();

            // Assert
            Assert.NotEmpty(descricao);
        }

        [Fact]
        public void LancamentoExtensions_ObterDescricaoOperacao_InvalidEnumArgumentException()
        {
            // Arrange
            Lancamento lancamento = new(1, null, Guid.NewGuid(), DateTime.UtcNow, TipoLancamento.Saida, (TipoOperacao)999, 100m);

            // Act
            void action () => lancamento.ObterDescricaoOperacao();

            // Assert
            Assert.Throws<InvalidEnumArgumentException>(action);
        }

        public static IEnumerable<object[]> TipoOperacaoEnumValues()
        {
            foreach (TipoOperacao tipoOperacao in Enum.GetValues<TipoOperacao>())
            {
                yield return new object[] { tipoOperacao, TipoLancamento.Entrada };
            }
            yield return new object[] { TipoOperacao.Transferencia, TipoLancamento.Saida };
        }
    }
}
