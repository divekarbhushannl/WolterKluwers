using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Utilities.Exceptions
{
    public class GatewayConnectionException : OrderException
    {
        public GatewayConnectionException(string exceptionMessage) : base(exceptionMessage)
        {
        }

        public GatewayConnectionException(string exceptionMessage, OrderException innerException) : base(exceptionMessage, innerException)
        {
        }
    }
}
