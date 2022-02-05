using System.ComponentModel.DataAnnotations;

namespace Gob.ContaBancaria.Domain.Requests
{
    public class DepositoRequest
    {
        [Range(1, int.MaxValue)]
        public int IdConta { get; set; }

        [Range(0.01, 1_000_000_000.00)]
        [RegularExpression(@"^[0-9]+(?:[.,][0-9]{1,2})?$", ErrorMessage = "Só são permitidos valores com no máximo duas casas decimais.")]
        public decimal Valor { get; set; }
    }
}
