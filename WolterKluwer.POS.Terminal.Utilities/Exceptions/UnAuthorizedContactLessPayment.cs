using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Utilities.Exceptions
{
    public class UnAuthorizedContactLessPayment : OrderException
    {
        public UnAuthorizedContactLessPayment(string exceptionMessage)
            : base(exceptionMessage)
        { }
    }
}
