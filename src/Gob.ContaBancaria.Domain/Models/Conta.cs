using System;
using System.Collections.Generic;

namespace Gob.ContaBancaria.Domain.Models
{
    public class Conta : Entity
    {
        protected Conta()
        {
        }

        public Conta(int idTitular, DateTime dataCriacao, Pessoa? titular)
        {
            IdTitular = idTitular;
            DataCriacao = dataCriacao;
            Titular = titular;
        }

        public int IdTitular { get; set; }
        public DateTime DataCriacao { get; set; }

        public virtual Pessoa? Titular { get; set; }
        public virtual ICollection<Lancamento>? Lancamentos { get; set; }
    }
}
