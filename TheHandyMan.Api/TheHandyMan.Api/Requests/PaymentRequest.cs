using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheHandyMan.Api.Requests
{
    public class PaymentRequest
    {
        public string token { get; set; }
        public int rate { get; set; }
    }
}