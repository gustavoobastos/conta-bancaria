using System;
using Gob.ContaBancaria.Domain.Models;

namespace Gob.ContaBancaria.Domain.OperacoesBancarias
{
    internal class Saque : Operacao
    {
        public Saque(decimal valorOperacao, decimal valorTaxa, int idConta)
        {
            ValorOperacao = valorOperacao;
            ValorTaxa = valorTaxa;

            CriarLancamentos(idConta);
        }

        private void CriarLancamentos(int idConta)
        {
            Guid idTransacao = Guid.NewGuid();
            DateTime dateTransacao = DateTime.UtcNow;

            _lancamentos.Add(new Lancamento(idConta, null, idTransacao, dateTransacao, TipoLancamento.Saida, TipoOperacao.Saque, ValorOperacao));
            _lancamentos.Add(new Lancamento(idConta, null, idTransacao, dateTransacao, TipoLancamento.Saida, TipoOperacao.TaxaSaque, ValorTaxa));
        }
    }
}
