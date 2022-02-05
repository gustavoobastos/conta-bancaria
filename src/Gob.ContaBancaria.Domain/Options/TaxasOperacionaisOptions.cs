namespace Gob.ContaBancaria.Domain.Options
{
    public class TaxasOperacionaisOptions
    {
        public const string TaxasOperacionais = "TaxasOperacionais";

        public decimal TaxaDeposito { get; set; }
        public decimal TaxaSaque { get; set; }
        public decimal TaxaTransferencia { get; set; }
    }
}
