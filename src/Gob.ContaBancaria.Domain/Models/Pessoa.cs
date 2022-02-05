using System.Collections.Generic;

namespace Gob.ContaBancaria.Domain.Models
{
    public class Pessoa : Entity
    {
        protected Pessoa()
        {
        }

        public Pessoa(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }

        public virtual ICollection<Conta>? Contas { get; set; }
    }
}
