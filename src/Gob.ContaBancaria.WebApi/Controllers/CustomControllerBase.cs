using System.Net.Mime;
using Gob.ContaBancaria.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Gob.ContaBancaria.WebApi.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public abstract class CustomControllerBase : ControllerBase
    {
        [NonAction]
        public ObjectResult BaseResult([ActionResultObjectValue] BaseResult result)
        {
            if (result.Sucesso)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
