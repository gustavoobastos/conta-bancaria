using System;
using Gob.ContaBancaria.Domain.Models;

namespace Gob.ContaBancaria.Domain.OperacoesBancarias
{
    internal class Deposito : Operacao
    {
        public override decimal ValorTotalOperacao => ValorOperacao - ValorTaxa;

        public Deposito(decimal valorOperacao, decimal valorTaxa, int idConta)
        {
            ValorOperacao = valorOperacao;
            ValorTaxa = Math.Round(valorOperacao * valorTaxa, 2, MidpointRounding.AwayFromZero);

            CriarLancamentos(idConta);
        }

        private void CriarLancamentos(int idConta)
        {
            Guid idTransacao = Guid.NewGuid();
            DateTime dateTransacao = DateTime.UtcNow;

            _lancamentos.Add(new Lancamento(idConta, null, idTransacao, dateTransacao, TipoLancamento.Entrada, TipoOperacao.Deposito, ValorOperacao));
            if (ValorTaxa >= 0.01m)
                _lancamentos.Add(new Lancamento(idConta, null, idTransacao, dateTransacao, TipoLancamento.Saida, TipoOperacao.TaxaDeposito, ValorTaxa));
        }
    }
}
