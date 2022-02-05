namespace Gob.ContaBancaria.Domain.ViewModels
{
    public class ExtratoViewModel
    {
        public string Data { get; set; }
        public string Operacao { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
