using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using CurrencyConverter.Core;
using CurrencyConverter.Core.Parsers;
using CurrencyConverter.Helper.ResultType;
using Xunit;

namespace CurrencyConverter.Test
{
    public class RCBTest
    {
        [Fact]
        public async void TestRCB()
        {
            var parser = new RCBParser();
            var rates  = await parser.GetCurrencyRatesAsync(DateTime.Now, new CancellationToken());
            Assert.True(rates.IsOk);
            Assert.NotEmpty(rates.Ok.Currencies);
        }
    }
}
