using System;

namespace CurrencyConverter.ViewModels
{
    public class CurrencyRatesRequest
    {
        public DateTime date { get; set; }
        public string bankCode { get; set; } // ECB, RCB

        public CurrencyRatesRequest( string _code, DateTime _date)
        {
            bankCode = _code;
            date = _date;
        }
    }
}
