using CurrencyExchange.Loggers;
using CurrencyExchange.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CurrencyExchange.NBPApi
{
    public static class NBPApiConnection
    {

        const string NBPurl = @"http://api.nbp.pl/api/exchangerates/rates/a/";

        public static NBPCurrencyRatesResponse GetCurrencyData(string currencyCode)
        {
            var requestUri = $"{NBPurl}{currencyCode}/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create( requestUri );
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                LogRequest(response);

            using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonResponse = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<NBPCurrencyRatesResponse>(jsonResponse);

                }
            }
        }


        private static void LogRequest( HttpWebResponse response )
        {

            NBPApiRequest request = new NBPApiRequest()
            {
                RequestUri = response.ResponseUri.ToString(),
                RequestDate = DateTime.Now,
                SuccessResponse = response.StatusCode == HttpStatusCode.OK
            };

            using (var ctx = new LogToDbContext())
            {
                ctx.OutRequests.Add(request);
                ctx.SaveChanges();
            }

        }

    }
}