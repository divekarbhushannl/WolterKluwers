using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Utilities.Exceptions
{
    public class RejectedCardException : OrderException
    {
        public RejectedCardException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}
