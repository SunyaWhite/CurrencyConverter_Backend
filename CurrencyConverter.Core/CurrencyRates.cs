using System;
using System.Xml;
using System.Collections.Generic;

namespace CurrencyConverter.Core
{
    public struct CurrencyRates
    {
        public IEnumerable<Currency> Currencies;
        public DateTime Date;

        public CurrencyRates(IEnumerable<Currency> currencies, DateTime date)
        {
            Currencies = currencies;
            Date = date;
        }
        
    }
}