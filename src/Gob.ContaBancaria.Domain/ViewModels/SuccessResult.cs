namespace Gob.ContaBancaria.Domain.ViewModels
{
    public class SuccessResult : BaseResult
    {
        public SuccessResult(string mensagem) : base(true)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; set; }
    }

    public class SuccessResult<T> : SuccessResult
    {
        public SuccessResult(T resposta) : base("Sucesso")
        {
            Resposta = resposta;
        }

        public T Resposta { get; set; }
    }
}
