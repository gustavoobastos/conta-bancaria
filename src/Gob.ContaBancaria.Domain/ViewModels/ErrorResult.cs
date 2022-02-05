namespace Gob.ContaBancaria.Domain.ViewModels
{
    public class ErrorResult : BaseResult
    {
        public ErrorResult(string erro) : base(false)
        {
            Erro = erro;
        }

        public string Erro { get; set; }
    }
}
