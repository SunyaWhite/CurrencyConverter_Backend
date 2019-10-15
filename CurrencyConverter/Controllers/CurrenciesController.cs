using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Core.Parsers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using CurrencyConverter.ViewModels;
using CurrencyConverter.Core;

namespace CurrencyConverter.Controllers
{
    [Route("/api/{Controller}")]
    [EnableCors("FrontView")]
    public class CurrenciesController: Controller
    {

        private IMemoryCache _cache { get; set; }

        public CurrenciesController(IMemoryCache cache)
        {
            _cache = cache;
        }
        // Should be replaced into other part of the project
        [NonAction]
        private ICurrencyParser SelectParser(string code)
        {
            try
            {
                switch (code)
                {
                    case "RCB":
                        return this.HttpContext.RequestServices.GetRequiredService<RCBParser>();
                    default:
                        return this.HttpContext.RequestServices.GetRequiredService<ECBParser>();
                }
            }
            catch(Exception exc)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCurrencies([FromBody] CurrencyRatesRequest request, CancellationToken cts)
        {

            if (request == null)
                request = new CurrencyRatesRequest("ECB", DateTime.Now);
            var value = new CurrencyRates();
            if (_cache.TryGetValue($"{request.bankCode}_{request.date.ToShortDateString()}", out value))
                return Ok(value);
            var parser = SelectParser(request.bankCode);
            var res = await parser.GetCurrencyRatesAsync(request.date, cts);
            if (res.IsOk)
                _cache.Set($"{request.bankCode}_{request.date.ToShortDateString()}", 
                    res.Ok,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2)));
            return res.Match<IActionResult>(
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