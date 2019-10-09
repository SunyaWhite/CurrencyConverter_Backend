using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Helper.ResultType;

namespace CurrencyConverter.Core.Parsers
{
    public interface ICurrencyParser
    {
        Task<Result<CurrencyRates>> GetCurrencyRatesAsync(DateTime date, CancellationToken cts = default(CancellationToken));
    }
}