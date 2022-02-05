namespace Gob.ContaBancaria.Domain.ViewModels
{
    public abstract class BaseResult
    {
        protected BaseResult(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public bool Sucesso { get; set; }
    }
}
