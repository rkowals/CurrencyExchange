using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CurrencyExchange.Models
{
    public class ApiRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RequestUri { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        public bool SuccessResponse { get; set; }
    }
}