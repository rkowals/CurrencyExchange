using CurrencyExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CurrencyExchange.Loggers
{
    public class LogToDbFilter : ActionFilterAttribute
    {

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            CurrencyApiRequest request = new CurrencyApiRequest() {
                RequestUri = filterContext.Request.RequestUri.ToString(),
                RequestDate = DateTime.Now,
                SuccessResponse = filterContext.Response.IsSuccessStatusCode
            };

            using (var ctx = new LogToDbContext())
            {
                ctx.InRequests.Add(request);
                ctx.SaveChanges();
            }

        }

    }
}