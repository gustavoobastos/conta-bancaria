using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.Models;
using Gob.ContaBancaria.Domain.Requests;
using Gob.ContaBancaria.Domain.ViewModels;

namespace Gob.ContaBancaria.Domain.Services
{
    public class ContaBancariaService : IContaBancariaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IContaRepository _contaRepository;
        private readonly ILancamentoRepository _lacamentoRepository;

        public ContaBancariaService(
            IPessoaRepository pessoaRepository,
            IContaRepository contaRepository,
            ILancamentoRepository lacamentoRepository
            )
        {
            _pessoaRepository = pessoaRepository;
            _contaRepository = contaRepository;
            _lacamentoRepository = lacamentoRepository;
        }

        public async Task<BaseResult> CriarContaAsync(CriarContaBancariaRequest request)
        {
            Pessoa? pessoa = await _pessoaRepository.BuscarPessoaAsync(request.CpfTitular);
            Conta conta = pessoa == null
                ? new(0, DateTime.UtcNow, new(request.NomeTitular, request.CpfTitular))
                : new(pessoa.Id, DateTime.UtcNow, null);

            await _contaRepository.CriarContaAsync(conta);

            return Result.ContaCriada();
        }

        public async Task<BaseResult> BuscarContasAsync()
        {
            IEnumerable<ContaViewModel> contas = await _contaRepository.BuscarContasAsync();

            return contas.Any()
                ? Result.SuccessResult(contas)
                : Result.NenhumaContaEncontrada();
        }

        public async Task<BaseResult> BuscarContaAsync(int idConta)
        {
            ContaViewModel? conta = await _contaRepository.BuscarContaAsync(idConta);

            return conta != null
                ? Result.SuccessResult(conta)
                : Result.ContaNaoExiste(idConta);
        }

        public async Task<BaseResult> SaldoAsync(int idConta)
        {
            decimal? saldoConta = await _contaRepository.BuscarSaldoContaAsync(idConta);

            return saldoConta.HasValue
                ? Result.SuccessResult(saldoConta.Value)
                : Result.ContaNaoExisteOuSemDeposito(idConta);
        }

        public async Task<BaseResult> ExtratoAsync(int idConta)
        {
            IEnumerable<ExtratoViewModel> extratos = await _lacamentoRepository.BuscarExtratoAsync(idConta);

            return extratos.Any()
                ? Result.SuccessResult(extratos)
                : Result.ContaNaoExisteOuSemDeposito(idConta);
        }
    }
}
