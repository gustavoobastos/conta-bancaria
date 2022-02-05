using System.Collections.Generic;
using Gob.ContaBancaria.Domain.Models;

namespace Gob.ContaBancaria.Domain.OperacoesBancarias
{
    public abstract class Operacao
    {
        protected readonly List<Lancamento> _lancamentos = new();

        public IReadOnlyCollection<Lancamento> Lacamentos => _lancamentos.AsReadOnly();
        public decimal ValorOperacao { get; protected set; }
        public decimal ValorTaxa { get; protected set; }
        public virtual decimal ValorTotalOperacao => ValorOperacao + ValorTaxa;
    }
}
