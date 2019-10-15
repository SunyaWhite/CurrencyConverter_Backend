using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Helper.ResultType;
using CurrencyConverter.Helper.TaskExtension;

namespace CurrencyConverter.Core.Parsers
{
    public abstract class AbstractParser : ICurrencyParser
    {

        protected string Url { get; set; }

        protected async Task<T> Retry<T>(Func<Task<T>> task, int reties, TimeSpan delay,
            CancellationToken cts = default(CancellationToken)) =>
            await task().ContinueWith(async innerTask =>
            {
                cts.ThrowIfCancellationRequested();
                if (innerTask.Status != TaskStatus.Faulted)
                    return await innerTask;
                if (reties == 0)
                    throw innerTask.Exception.InnerException ?? new Exception("Task has failed");
                return await Retry<T>(task, reties - 1, delay);
            }).Unwrap();

        public virtual async Task<Result<CurrencyRates>> GetCurrencyRatesAsync(DateTime date, CancellationToken cts)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                SetDateInQuery(date);
                return await Retry(() => GetRawDataAsync(), 3, new TimeSpan(0, 0, 0, 2), cts)
                    .Bind((async rawData =>
                    {
                        return await rawData.Match(
                            async stream => await ParseAsync(stream),
                            error => throw error);
                    }))
                    .Bind(async currencies => 
                    {
                        return await currencies.Match(
                            async curs => new CurrencyRates(curs, date),
                            error => throw error);
                    });
                //var rawDataResult = await Retry(() => GetRawData(), 3, new TimeSpan(0, 0, 0, 2), cts);
                //var rates = await rawDataResult.Match(async rawData => await Parse(rawData), error => throw error);
                //return rates.Match(currencyRates => new CurrencyRates(currencyRates, date, "EUR"), error => throw error);
            }
            catch (Exception exc)
            {
                return exc;
            }
        }

        protected abstract Task<Result<IEnumerable<Currency>>> ParseAsync(Stream streamData);

        protected abstract void SetDateInQuery(DateTime date);

        protected virtual async Task<Result<Stream>> GetRawDataAsync()
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync(Url).Bind<HttpResponseMessage, Result<Stream>>(async response =>
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                        return await response.Content.ReadAsStreamAsync();
                    return new Exception($"Unable to read data from url : {Url} . Status code : {response.StatusCode}");
                });
            }
        }
    }
}