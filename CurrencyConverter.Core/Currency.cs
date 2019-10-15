namespace CurrencyConverter.Core
{
    public struct Currency
    {
        public string Code { get; set; }
        public float Rate { get; set; }

        public Currency(string code, float rate)
        {
            Code = code;
            Rate = rate;
        }
    }
}