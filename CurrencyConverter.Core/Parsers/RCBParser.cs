using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Helper.ResultType;

namespace CurrencyConverter.Core.Parsers
{
    public class RCBParser : AbstractParser
    {

        public RCBParser()
        {
            Url = "http://www.cbr.ru/scripts/XML_daily.asp";
        }

        protected override async Task<Result<IEnumerable<Currency>>> ParseAsync(Stream streamData)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(streamData);
                var nodes = new List<XmlNode>(doc.DocumentElement.GetElementsByTagName("Valute").OfType<XmlNode>());
                var rates = nodes.AsParallel().Aggregate(new List<Currency>(), (list, node) =>
                {
                    var children = new List<XmlNode>(node.SelectNodes("./CharCode | ./Nominal | ./Value").OfType<XmlNode>());
                    if (children[0] == null || children[1] == null || children[2] == null)
                        return list;
                    list.Add(new Currency(children[0].InnerText, float.Parse(children[1].InnerText) / float.Parse(children[2].InnerText.Replace(',', '.'))));
                    return list;
                });
                rates.Add(new Currency("RUB", 1));
                return rates;
            }
            catch(Exception exc)
            {
                return exc;
            }
        }

        protected override void SetDateInQuery(DateTime date) => Url += $"?date_req={date.Day}/{date.Month}/{date.Year}";
    }
}
