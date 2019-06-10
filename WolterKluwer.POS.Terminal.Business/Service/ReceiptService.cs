using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Entities;
using WolterKluwer.POS.Terminal.Receipts.ReceiptAPI;
using WolterKluwer.POS.Terminal.Utilities;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class ReceiptService : IReceiptService
    {
        readonly ReceiptGenerate rptGenerate = new ReceiptGenerate();

        /// <summary>
        /// PrintReceipt
        /// </summary>
        /// <param name="order"></param>
        /// <returns>receipt</returns>
        public Receipt PrintReceipt(Order order)
        {
            var receipt = new Receipt
            {
                Title = "Receipt for your order placed on " + DateTime.Now,
                Body = "Your order details: \n "
            };

            foreach (var orderItem in order.Items)
            {
                receipt.Body += "ProductCode:"+ orderItem.ProductCode+ " Quantity : " + orderItem.Quantity + "\n";
                receipt.Body += "---------------------------------------------------------------\n";
            }
            receipt.Body += "Total Amount :$" + order.TotalAmount;
            try
            {
                return rptGenerate.Print(receipt);
            }
            catch (Exception ex)
            {
                Logger.Error("Problem sending to printer", ex);
            }
            return rptGenerate.Print(receipt);
        }
    }
}

