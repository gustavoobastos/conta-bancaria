using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.Requests;
using Gob.ContaBancaria.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gob.ContaBancaria.WebApi.Controllers
{
    [Route("api/contas-bancarias")]
    public class ContasBancariasController : CustomControllerBase
    {
        private readonly IContaBancariaService _contaBancariaService;

        public ContasBancariasController(IContaBancariaService contaBancariaService)
        {
            _contaBancariaService = contaBancariaService;
        }

        /// <summary>
        /// Exibir todas as contas bancarias
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult<IEnumerable<ContaViewModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Exibir()
        {
            BaseResult result = await _contaBancariaService.BuscarContasAsync();

            return BaseResult(result);
        }

        /// <summary>
        /// Exibir conta bancaria
        /// </summary>
        /// <param name="idConta">Id da conta bancaria</param>
        [HttpGet("{idConta}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult<ContaViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Exibir([Range(1, int.MaxValue)] int idConta)
        {
            BaseResult result = await _contaBancariaService.BuscarContaAsync(idConta);

            return BaseResult(result);
        }

        /// <summary>
        /// Criar conta bancaria
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Criar([FromBody] CriarContaBancariaRequest request)
        {
            BaseResult result = await _contaBancariaService.CriarContaAsync(request);

            return BaseResult(result);
        }

        /// <summary>
        /// Exibir saldo da conta bancaria
        /// </summary>
        /// <param name="idConta">Id da conta bancaria</param>
        [HttpGet("saldo/{idConta}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult<decimal>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Saldo([Range(1, int.MaxValue)] int idConta)
        {
            BaseResult result = await _contaBancariaService.SaldoAsync(idConta);

            return BaseResult(result);
        }

        /// <summary>
        /// Exibir extrato da conta bancaria
        /// </summary>
        /// <param name="idConta">Id da conta bancaria</param>
        [HttpGet("extrato/{idConta}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult<ExtratoViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Extrato([Range(1, int.MaxValue)] int idConta)
        {
            BaseResult result = await _contaBancariaService.ExtratoAsync(idConta);

            return BaseResult(result);
        }
    }
}
