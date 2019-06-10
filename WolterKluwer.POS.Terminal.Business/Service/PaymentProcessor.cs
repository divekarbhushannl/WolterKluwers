using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Entities;
using WolterKluwer.POS.Terminal.Utilities.Exceptions;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class PaymentProcessor : IPaymentProcessor
    {
        /// <summary>
        /// ChargeCard
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>

        public void ChargeCard(PaymentDetails paymentDetails, decimal totalAmount)
        {
            using (var ccMachine = new DebitCardMachine())
            {
                try
                {
                    ccMachine.CardNumber = paymentDetails.DebitCardNumber;
                    ccMachine.ExpiresMonth = paymentDetails.ExpiresMonth;
                    ccMachine.ExpiresYear = paymentDetails.ExpiresYear;
                    ccMachine.NameOnCard = paymentDetails.CardholderName;
                    ccMachine.AmountToCharge = totalAmount;

                    ccMachine.Charge();
                }
                catch (RejectedCardException ex)
                {
                    throw new OrderException("The card gateway rejected the card.", ex);
                }
            }
        }

    }
}
