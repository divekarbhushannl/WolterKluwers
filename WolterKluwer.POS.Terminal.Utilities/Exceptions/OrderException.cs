using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Utilities.Exceptions
{
    public class OrderException : Exception
    {
        public OrderException(string exceptionMessage) :
            base(exceptionMessage)
        { }

        public OrderException(string exceptionMessage, OrderException innerException) :
            base(exceptionMessage, innerException)
        { }
    }
}
