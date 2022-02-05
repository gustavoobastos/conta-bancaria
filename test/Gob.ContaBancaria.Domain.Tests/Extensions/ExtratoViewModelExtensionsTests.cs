using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Gob.ContaBancaria.Domain.Extensions;
using Gob.ContaBancaria.Domain.ViewModels;
using Xunit;

namespace Gob.ContaBancaria.Domain.Tests.Extensions
{
    public class ExtratoViewModelExtensionsTests
    {
        [Fact]
        public void ExtratoViewModelExtensions_CalcularSaldo_Sucesso()
        {
            // Arrange
            Fixture fixture = new();
            IEnumerable<ExtratoViewModel> viewsModels = fixture.CreateMany<ExtratoViewModel>(99);

            // Act
            decimal saldo = viewsModels.CalcularSaldo().Last().Saldo;

            // Assert
            Assert.Equal(viewsModels.Sum(x => x.Valor), saldo);
        }
    }
}
