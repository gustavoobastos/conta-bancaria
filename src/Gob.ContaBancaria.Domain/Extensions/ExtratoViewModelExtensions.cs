using System.Collections.Generic;
using Gob.ContaBancaria.Domain.ViewModels;

namespace Gob.ContaBancaria.Domain.Extensions
{
    public static class ExtratoViewModelExtensions
    {
        public static IEnumerable<ExtratoViewModel> CalcularSaldo(this IEnumerable<ExtratoViewModel> extratoViewModels)
        {
            decimal saldo = decimal.Zero;
            foreach (ExtratoViewModel extrato in extratoViewModels)
            {
                saldo += extrato.Valor;
                extrato.Saldo = saldo;

                yield return extrato;
            }
        }
    }
}
