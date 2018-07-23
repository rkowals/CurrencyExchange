using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchange.Models
{
    public class ExchangeResponse
    {
        public ExchangeRequest Request { get; set; }
        public double Ratio { get; set; }
        public double Result { get; set; }
    }
}