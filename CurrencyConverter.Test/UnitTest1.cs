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
    public class ECBTest
    {
        /*[Fact]
        public async void Test1()
        {
            var parser = new ECBParser();
            var res = await parser.GetRawData();
            Assert.True(res.IsOk);
            //Assert.NotEmpty(re);
        }

        [Fact]
        public async void Test2()
        {
            var parser = new ECBParser();
            var res = await parser.GetRawData();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(res.Ok);
            var nodes = new List<XmlNode>(xmlDoc.DocumentElement.GetElementsByTagName("Cube").OfType<XmlNode>());
            /*foreach (var node in nodes)
            {
                Console.WriteLine((node as XmlElement).Name);
            }
            var curs = nodes.AsParallel().Aggregate(new List<Currency>(), (list, node) =>
            {
                if ((node as XmlElement).HasAttribute("rate"))
                    list.Add(
                        new Currency((node as XmlElement).GetAttribute("currency"),
                            decimal.Parse((node as XmlElement).GetAttribute("rate"))));
                return list;
            });
            foreach (var cur in curs)
            {
                Console.WriteLine($"{cur.Code} {cur.Rate}");
            }
            Assert.True(res.IsOk);
        }*/

        [Fact]
        public async void Test3()
        {
            var parser = new ECBParser();
            var cts = new CancellationToken();
            var res = await parser.GetCurrencyRatesAsync(DateTime.Now, cts);
            var a = 10;
            Assert.True(res.IsOk);
        }    
    }
}