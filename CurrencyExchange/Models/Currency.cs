using CurrencyExchange.NBPApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CurrencyExchange.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double Rate { get; set; }

        private DateTime _date;

        public Currency()
        {
            this._date = new DateTime(2000, 1, 1);
        }


        public bool IsUpToDate()
        {
                return DateTime.Now.Subtract( this._date ).Hours > 12 ;
        }

        public bool Update()
        {
            NBPCurrencyRatesResponse currencyData;

            try
            {
               currencyData =  NBPApiConnection.GetCurrencyData( this.Code );
            }
            catch( WebException ex)
            {
                return false;
            }


            this.Rate = currencyData.Rates.First().Mid;
            this._date = currencyData.Rates.First().EffectiveDate;
            return true;


        }


    }
}