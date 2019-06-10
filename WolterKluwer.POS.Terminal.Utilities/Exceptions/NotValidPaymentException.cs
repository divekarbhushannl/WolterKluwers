using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Utilities.Exceptions
{
    public class NotValidPaymentException : OrderException
    {
        public NotValidPaymentException(string message) : base(message)
        {

        }
    }
}
