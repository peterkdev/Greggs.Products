using System;
namespace Greggs.Products.Api.Currency
{
    public interface ICurrencyConverter
    {
        decimal Convert(decimal price);
    }
}