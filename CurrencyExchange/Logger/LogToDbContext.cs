using CurrencyExchange.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CurrencyExchange.Loggers
{
    public class LogToDbContext : DbContext
    {
        
        public virtual DbSet<CurrencyApiRequest> InRequests { get; set; }
        public virtual DbSet<NBPApiRequest> OutRequests { get; set; }

    }
}