using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Currency;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IDataAccess<Product> _dataAccess;
        private readonly ICurrencyConverter _currencyConverter;

        public ProductController(ILogger<ProductController> logger, IDataAccess<Product> dataAccess, ICurrencyConverter currencyConverter)
        {
            _logger = logger;
            _dataAccess = dataAccess;
            _currencyConverter = currencyConverter;
        }

        [HttpGet]
        public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5, bool applyCurrencyConversion = false)
        {
            //Get the latest menu of products (I am assuming that is what _dataAccess.List() returns) rather than the random static products
            //The api returns the products but it use the data access functionality implemented
            var productList = _dataAccess.List(pageStart, pageSize);

            if (!applyCurrencyConversion)
                return productList;

            //Get the price of the products returned in Euros when required
            return productList.Select(product => new Product
            {
                Name = product.Name,
                Price = _currencyConverter.Convert(product.Price),
            }).ToList();
        }
    }
}
