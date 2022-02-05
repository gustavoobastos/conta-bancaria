using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.OperacoesBancarias;
using Gob.ContaBancaria.Domain.Requests;
using Gob.ContaBancaria.Domain.ViewModels;

namespace Gob.ContaBancaria.Domain.Services
{
    public class OperacaoBancariaService : IOperacaoBancariaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IOperacaoFactory _operacaoFactory;

        public OperacaoBancariaService(
            IContaRepository contaRepository,
            ILancamentoRepository lancamentoRepository,
            IOperacaoFactory operacaoFactory)
        {
            _contaRepository = contaRepository;
            _lancamentoRepository = lancamentoRepository;
            _operacaoFactory = operacaoFactory;
        }

        public async Task<BaseResult> DepositarAsync(DepositoRequest request)
        {
            bool contaExiste = await _contaRepository.ContaExisteAsync(request.IdConta);
            if (!contaExiste) return Result.ContaNaoExiste(request.IdConta);

            Operacao operacao = _operacaoFactory.CriarDeposito(request.Valor, request.IdConta);

            await _lancamentoRepository.SalvarLancamentosAsync(operacao.Lacamentos);

            return Result.DepositoEfetuado();
        }

        public async Task<BaseResult> SacarAsync(SaqueRequest request)
        {
            bool contaExiste = await _contaRepository.ContaExisteAsync(request.IdConta);
            if (!contaExiste) return Result.ContaNaoExiste(request.IdConta);

            Operacao operacao = _operacaoFactory.CriarSaque(request.Valor, request.IdConta);

            decimal saldoConta = (await _contaRepository.BuscarSaldoContaAsync(request.IdConta)).GetValueOrDefault();
            if (operacao.ValorTotalOperacao > saldoConta) return Result.SaldoInsuficiente(operacao.ValorTotalOperacao, saldoConta);

            await _lancamentoRepository.SalvarLancamentosAsync(operacao.Lacamentos);

            return Result.SaqueEfetuado();
        }

        public async Task<BaseResult> TransferirAsync(TransferenciaRequest request)
        {
            if (request.IdContaOrigem == request.IdContaDestino) return Result.TransferenciaParaMesmaConta();

            bool contaOrigemExiste = await _contaRepository.ContaExisteAsync(request.IdContaOrigem);
            if (!contaOrigemExiste) return Result.ContaNaoExiste(request.IdContaOrigem);

            bool contaDestinoExiste = await _contaRepository.ContaExisteAsync(request.IdContaDestino);
            if (!contaDestinoExiste) return Result.ContaNaoExiste(request.IdContaDestino);

            Operacao operacao = _operacaoFactory.CriarTransferencia(request.Valor, request.IdContaOrigem, request.IdContaDestino);

            decimal saldoConta = (await _contaRepository.BuscarSaldoContaAsync(request.IdContaOrigem)).GetValueOrDefault();
            if (operacao.ValorTotalOperacao > saldoConta) return Result.SaldoInsuficiente(operacao.ValorTotalOperacao, saldoConta);

            await _lancamentoRepository.SalvarLancamentosAsync(operacao.Lacamentos);

            return Result.TransferenciaEfetuada();
        }
    }
}
