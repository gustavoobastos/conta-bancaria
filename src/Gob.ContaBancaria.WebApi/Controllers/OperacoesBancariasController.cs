using System.Threading.Tasks;
using Gob.ContaBancaria.Domain.Interfaces;
using Gob.ContaBancaria.Domain.Requests;
using Gob.ContaBancaria.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gob.ContaBancaria.WebApi.Controllers
{
    [Route("api/operacoes-bancarias")]
    public class OperacoesBancariasController : CustomControllerBase
    {
        private readonly IOperacaoBancariaService _operacaoBancariaService;

        public OperacoesBancariasController(IOperacaoBancariaService operacaoBancariaService)
        {
            _operacaoBancariaService = operacaoBancariaService;
        }

        /// <summary>
        /// Depositar
        /// </summary>
        [HttpPost("depositar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Depositar([FromBody] DepositoRequest request)
        {
            BaseResult result = await _operacaoBancariaService.DepositarAsync(request);

            return BaseResult(result);
        }

        /// <summary>
        /// Sacar
        /// </summary>
        [HttpPost("sacar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Sacar([FromBody] SaqueRequest request)
        {
            BaseResult result = await _operacaoBancariaService.SacarAsync(request);

            return BaseResult(result);
        }

        /// <summary>
        /// Transferir
        /// </summary>
        [HttpPost("transferir")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> Transferir([FromBody] TransferenciaRequest request)
        {
            BaseResult result = await _operacaoBancariaService.TransferirAsync(request);

            return BaseResult(result);
        }
    }
}
