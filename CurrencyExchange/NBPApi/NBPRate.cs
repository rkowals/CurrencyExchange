using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyExchange.Models
{
    public class NBPRate
    {
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public double Mid { get; set; }
    }
}