using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Utilities.Exceptions
{
    public class UnauthorizedCashPayment : OrderException
    {

        public UnauthorizedCashPayment(string exceptionMessage)
            : base(exceptionMessage)
        { }
    }
}
