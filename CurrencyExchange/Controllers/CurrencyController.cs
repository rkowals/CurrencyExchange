using CurrencyExchange.Loggers;
using CurrencyExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace CurrencyExchange.Controllers
{
    [LogToDbFilter]
    public class CurrencyController : ApiController
    {

        private IEnumerable<Currency> _currencies = new List<Currency>()
        {
            new Currency(){ Name = "dolar amerykański", Code = "usd", Rate = 1.23  },
            new Currency(){ Name = "euro", Code = "eur", Rate = 2.655  },
            new Currency(){ Name = "frank szwajcarski", Code = "chf", Rate = 1.78  },
            new Currency(){ Name = "funt szterling", Code = "gbp", Rate = 2.1  },
            new Currency(){ Name = "korona czeska", Code = "czk", Rate = 1.145  },
            new Currency(){ Name = "korona duńska", Code = "dkk", Rate = 1.965  }
        };

      /// <summary>
      /// Get currency data
      /// </summary>
      /// <remarks>
      /// Get details for a specific currency 
      /// </remarks>
      /// <param name="code">Currency code</param>
      /// <returns></returns>
      /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(Currency))]
        [Route("currency/{code}")]
        public IHttpActionResult CurrencyRate(string code)
        {
            if (String.IsNullOrWhiteSpace(code))
                return BadRequest();

            var currency = _currencies
                .Where(curr => curr.Code == code)
                .SingleOrDefault();

            if (currency == null)
                return NotFound();

            return Ok(currency);
        }

        /// <summary>
        /// List currencies
        /// </summary>
        /// <remarks>
        /// Get a list of all currencies
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Currency>))]
        [Route("currency")]
        public IHttpActionResult AllCurrenciesRates()
        {

            foreach (var currency in _currencies)
                if (!currency.IsUpToDate())
                    currency.Update();

            return Ok( _currencies );
        }


        /// <summary>
        /// Calculate result of specified currency exchange
        /// </summary>
        /// <param name="from">Code of the currency being sold</param>
        /// <param name="to">Code of the currency being bought</param>
        /// <param name="amount">Amount of the currency being sold </param>
        /// <returns></returns>
        [ResponseType(typeof(ExchangeResponse))]
        [HttpGet]
        [Route("currency/exchange")]
        public IHttpActionResult Exchange(string from, string to, double? amount)
        {

            if (String.IsNullOrWhiteSpace(from) || String.IsNullOrWhiteSpace(to) || !amount.HasValue)
                return BadRequest();

            if (amount < 0)
                return BadRequest();

            var exchange = new ExchangeRequest() {
                From = from, To = to, Amount = amount.Value
            };

            var  FromCurrency = _currencies
                .Where(curr => curr.Code.Equals(exchange.From))
                .SingleOrDefault();

            var  ToCurrency = _currencies
                .Where(curr => curr.Code.Equals(exchange.To))
                .SingleOrDefault();

            if (FromCurrency == null || ToCurrency == null)
                return NotFound();

            if (!FromCurrency.IsUpToDate())
                FromCurrency.Update();

            if (!ToCurrency.IsUpToDate())
                ToCurrency.Update();

            var exchangeResponse = new ExchangeResponse() {
                Request = exchange
            };

            exchangeResponse.Ratio = FromCurrency.Rate / ToCurrency.Rate;
            exchangeResponse.Result = exchangeResponse.Ratio * exchange.Amount;

            return Ok( exchangeResponse );
        }


    }
}