namespace CurrencyConverter.Core
{
    public struct Currency
    {
        public string Code { get; set; }
        public decimal Rate { get; set; }

        public Currency(string code, decimal rate)
        {
            Code = code;
            Rate = rate;
        }
    }
}