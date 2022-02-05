using System.ComponentModel.DataAnnotations;

namespace Gob.ContaBancaria.Domain.Requests
{
    public class CriarContaBancariaRequest
    {
        [Required]
        [StringLength(255)]
        public string NomeTitular { get; set; }

        [Required]
        [RegularExpression("[0-9]{11}")]
        public string CpfTitular { get; set; }
    }
}
