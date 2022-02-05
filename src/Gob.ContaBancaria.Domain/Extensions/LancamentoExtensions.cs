using System.ComponentModel;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Domain.ViewModels;

namespace Gob.ContaBancaria.Domain.Extensions
{
    public static class LancamentoExtensions
    {
        public static ExtratoViewModel ToExtratoViewModel(this Lancamento lancamento)
        {
            return new ExtratoViewModel()
            {
                Data = lancamento.Data.ToString("dd/MM/yyyy HH:mm:ss"),
                Operacao = lancamento.ObterDescricaoOperacao(),
                Valor = lancamento.TipoLancamento == TipoLancamento.Entrada ? lancamento.Valor : lancamento.Valor * -1,
                Saldo = decimal.Zero
            };
        }

        internal static string ObterDescricaoOperacao(this Lancamento lancamento)
        {
            return lancamento.TipoOperacao switch
            {
                TipoOperacao.Deposito => "Deposito",
                TipoOperacao.Saque => "Saque",
                TipoOperacao.Transferencia => (lancamento.TipoLancamento == TipoLancamento.Entrada) ? "Transferência recebida" : "Transferência enviada",
                TipoOperacao.TaxaDeposito => "Taxa de Deposito",
                TipoOperacao.TaxaSaque => "Taxa de Saque",
                TipoOperacao.TaxaTransferencia => "Taxa de Transferência",
                _ => throw new InvalidEnumArgumentException(nameof(TipoOperacao), (int)lancamento.TipoOperacao, typeof(TipoOperacao)),
            };
        }
    }
}
