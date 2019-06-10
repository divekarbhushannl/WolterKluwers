using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Entities
{
    public class PaymentDetails
    {
        public PaymentMethod PaymentMethod { get; set; }
        public string DebitCardNumber { get; set; }
        public string ExpiresMonth { get; set; }
        public string ExpiresYear { get; set; }
        public string CardholderName { get; set; }
        public string Credentials { get; set; }
    }
}
