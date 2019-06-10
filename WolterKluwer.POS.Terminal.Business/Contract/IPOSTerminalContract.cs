using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Business.Service;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.Business.Contract
{
    public interface IPOSTerminalContract
    {
        Order CalculateTotalPrice(Order order);
    }
}
