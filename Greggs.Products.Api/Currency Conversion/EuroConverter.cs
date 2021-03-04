using System;
namespace Greggs.Products.Api.Currency
{
    public class EuroConverter : ICurrencyConverter
    {
        private const decimal exchangeRate = 1.11m;

        //Convert to Euro based on the constant exchange rate
        public decimal Convert(decimal price)
        {
            return Round(price * exchangeRate);
        }

        private decimal Round(decimal price)
        {
            return Math.Round(price, 2);
        }
    }
}
