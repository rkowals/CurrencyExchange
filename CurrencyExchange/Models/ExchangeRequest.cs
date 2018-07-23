using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CurrencyExchange.Models
{
    public class ExchangeRequest
    {

        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }

    }
}