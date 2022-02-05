using System;
using Gob.ContaBancaria.Domain.Models;

namespace Gob.ContaBancaria.Domain.OperacoesBancarias
{
    internal class Transferencia : Operacao
    {
        public Transferencia(decimal valorOperacao, decimal valorTaxa, int idConta, int idContaDestino)
        {
            ValorOperacao = valorOperacao;
            ValorTaxa = valorTaxa;

            CriarLancamentos(idConta, idContaDestino);
        }

        private void CriarLancamentos(int idConta, int idContaDestino)
        {
            Guid idTransacao = Guid.NewGuid();
            DateTime dateTransacao = DateTime.UtcNow;

            _lancamentos.Add(new Lancamento(idConta, null, idTransacao, dateTransacao, TipoLancamento.Saida, TipoOperacao.Transferencia, ValorOperacao));
            _lancamentos.Add(new Lancamento(idConta, null, idTransacao, dateTransacao, TipoLancamento.Saida, TipoOperacao.TaxaTransferencia, ValorTaxa));
            _lancamentos.Add(new Lancamento(idContaDestino, idConta, idTransacao, dateTransacao, TipoLancamento.Entrada, TipoOperacao.Transferencia, ValorOperacao));
        }
    }
}
