using System.ComponentModel.DataAnnotations;

namespace Gob.ContaBancaria.Domain.Requests
{
    public class TransferenciaRequest
    {
        [Range(1, int.MaxValue)]
        public int IdContaOrigem { get; set; }

        [Range(1, int.MaxValue)]
        public int IdContaDestino { get; set; }

        [Range(0.01, 1_000_000_000.00)]
        [RegularExpression(@"^[0-9]+(?:[.,][0-9]{1,2})?$", ErrorMessage = "Só são permitidos valores com no máximo duas casas decimais.")]
        public decimal Valor { get; set; }
    }
}
