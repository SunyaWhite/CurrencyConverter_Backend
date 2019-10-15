using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Core.Parsers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using CurrencyConverter.ViewModels;

namespace CurrencyConverter.Controllers
{
    [Route("/api/{Controller}")]
    [EnableCors("FrontView")]
    public class CurrenciesController: Controller
    {
        // Should be replaced into other part of the project
        [NonAction]
        public ICurrencyParser SelectParser(string code)
        {
            switch(code)
            {
                case "RCB":
                    return this.HttpContext.RequestServices.GetRequiredService<RCBParser>();
                default:
                    return this.HttpContext.RequestServices.GetRequiredService<ECBParser>();
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCurrencies([FromBody] CurrencyRatesRequest request, CancellationToken cts)
        {
            var parser = SelectParser(request.bankCode);
            return (await parser.GetCurrencyRatesAsync(request.date, cts))
                .Match<IActionResult>(
                    currencies => Ok(currencies),
                    error => BadRequest(error));
        }

        /*[HttpGet]
        public async Task<IActionResult> Get(CancellationToken cts)
        {
            var parser = this.ControllerContext.HttpContext.RequestServices.GetRequiredService<ICurrencyParser>();
            return (await parser.GetCurrencyRatesAsync(DateTime.Now, cts))
                .Match<IActionResult>(
                    currencies => Ok(currencies),
                    error => BadRequest(error));
        }*/
    }
}