using System;
using System.Threading.Tasks;
using CurrencyConverter.Helper.ResultType;
using CurrencyConverter.Core.Parsers;
using Xunit;

namespace CurrencyConverter.Tests
{
    public class ECBParserTest
    {
        [Fact]
        public async void DownloadData()
        {
            var parser = new ECBParser();
            var res = await parser.GetRawData();
            Assert.True(res.IsOk);
            Assert.NotEmpty(res.Ok);
        }
        
    }
}