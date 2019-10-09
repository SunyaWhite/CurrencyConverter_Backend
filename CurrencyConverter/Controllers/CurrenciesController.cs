using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Core.Parsers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.Controllers
{
    [Route("/api/{Controller}")]
    [EnableCors("FrontView")]
    public class CurrenciesController: Controller
    {
        /*[HttpPost]
        public async Task<IActionResult> GetCurrencies([FromBody] DateTime date, CancellationToken cts)
        {
            var parser = this.ControllerContext.HttpContext.RequestServices.GetRequiredService<ICurrencyParser>();
            return (await parser.GetCurrencyRatesAsync(date, cts))
                .Match<IActionResult>(
                    currencies => Ok(currencies),
                    error => BadRequest(error));
        }*/

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cts)
        {
            var parser = this.ControllerContext.HttpContext.RequestServices.GetRequiredService<ICurrencyParser>();
            return (await parser.GetCurrencyRatesAsync(DateTime.Now, cts))
                .Match<IActionResult>(
                    currencies => Ok(currencies),
                    error => BadRequest(error));
        }
    }
}