using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolterKluwer.POS.Terminal.Business.Service;

namespace WolterKluwer.POS.Terminal.Receipts.ReceiptAPI
{
    public class ReceiptGenerate : IPrint
    {
        public Receipt Print(Receipt receipt)
        {
            return receipt;
        }
    }
}
