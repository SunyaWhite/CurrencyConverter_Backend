using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Threading;
using System.Threading.Tasks;
using CurrencyConverter.Helper.ResultType;
using CurrencyConverter.Helper.TaskExtension;

namespace CurrencyConverter.Core.Parsers
{
    public class ECBParser : AbstractParser
    {
        public ECBParser()
        {
            Url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        }

        protected override async Task<Result<IEnumerable<Currency>>> ParseAsync(Stream streamData)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(streamData);
                var rates = new List<XmlNode>(xmlDoc.DocumentElement.GetElementsByTagName("Cube").OfType<XmlNode>())
                    .Aggregate(new List<Currency>(), (list, node) =>
                {
                    if (node != null && (node as XmlElement).HasAttribute("rate"))
                        list.Add(
                            new Currency((node as XmlElement).GetAttribute("currency"),
                                decimal.Parse((node as XmlElement).GetAttribute("rate"))));
                    return list;
                });
                rates.Add(new Currency("EUR", 1));
                return rates;
            }
            catch (Exception exc)
            {
                return exc;
            }
        }
    }
}