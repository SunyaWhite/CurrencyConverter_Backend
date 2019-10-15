using System;

namespace CurrencyConverter.ViewModels
{
    public class CurrencyRatesRequest
    {
        public DateTime date { get; set; }
        public string bankCode { get; set; } // ECB, RCB
    }
}
