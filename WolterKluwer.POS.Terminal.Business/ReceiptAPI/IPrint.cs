using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolterKluwer.POS.Terminal.Business;
using WolterKluwer.POS.Terminal.Business.Service;

namespace WolterKluwer.POS.Terminal.Receipts.ReceiptAPI
{
    public interface IPrint
    {
        Receipt Print(Receipt receipt);
    }
}
