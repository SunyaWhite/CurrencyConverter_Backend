using System;
using System.Xml;
using System.Collections.Generic;

namespace CurrencyConverter.Core
{
    public struct CurrencyRates
    {
        public IEnumerable<Currency> Currencies;
        public DateTime Date;
        public string mainCurrencyCode;

        public CurrencyRates(IEnumerable<Currency> currencies, DateTime date, string code)
        {
            Currencies = currencies;
            Date = date;
            mainCurrencyCode = code;
        }
        
    }
}