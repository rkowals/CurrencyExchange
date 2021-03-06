﻿using CurrencyExchange.NBPApi;
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
        
        private DateTime _date = new DateTime(2000, 1, 1);
        private DateTime _lastUpdateTry;

        private double _rate;

        public double Rate
        {
            get
            {
                var k = 8;
                if ( (IsUpToDate() == false) && (HoursFromLastUpdateTry() >= 1))
                    this.Update();

                return _rate;

            }

            set
            {
                this._rate = value;
            }
        }

        private double HoursFromLastUpdateTry() => DateTime.Now.Subtract( this._lastUpdateTry).TotalHours;


        public bool IsUpToDate()
        {
            return DateTime.Now.Subtract(this._date).TotalDays < 1.0;
        }

        public bool Update()
        {
            NBPCurrencyRatesResponse currencyData;
            this._lastUpdateTry = DateTime.Now;

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