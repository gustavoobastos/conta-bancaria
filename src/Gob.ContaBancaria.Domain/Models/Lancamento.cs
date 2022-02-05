using System;

namespace Gob.ContaBancaria.Domain.Models
{
    public class Lancamento : Entity
    {
        protected Lancamento()
        {
        }

        public Lancamento(int idConta, int? idContaOrigem, Guid idTransacao, DateTime data, TipoLancamento tipoLancamento, TipoOperacao tipoOperacao, decimal valor)
        {
            IdConta = idConta;
            IdContaOrigem = idContaOrigem;
            IdTransacao = idTransacao;
            Data = data;
            TipoLancamento = tipoLancamento;
            TipoOperacao = tipoOperacao;
            Valor = valor;
        }

        public int IdConta { get; set; }
        public int? IdContaOrigem { get; set; }
        public Guid IdTransacao { get; set; }
        public DateTime Data { get; set; }
        public TipoLancamento TipoLancamento { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public decimal Valor { get; set; }

        public virtual Conta? Conta { get; set; }
        public virtual Conta? ContaOrigem { get; set; }
    }
}
