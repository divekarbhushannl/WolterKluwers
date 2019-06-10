using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Entities;
using WolterKluwer.POS.Terminal.Utilities;
using WolterKluwer.POS.Terminal.Utilities.Exceptions;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentProcessor paymentProcessor;

        public PaymentService(IPaymentProcessor processor)
        {
            paymentProcessor = processor;
        }

        /// <summary>
        /// Charge
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <param name="totalAmount"></param>
        /// <param name="Message"></param>
        /// <returns></returns>

        public void Charge(PaymentDetails paymentDetails, decimal totalAmount,ref string Message)
        {
            if (paymentDetails.PaymentMethod == PaymentMethod.Debitcard)
            {
                paymentProcessor.ChargeCard(paymentDetails, totalAmount);
            }
            else if (paymentDetails.PaymentMethod == PaymentMethod.Cash)
            {
                Message = AuthorizeCashPayment(totalAmount);
            }
            else
            {
                throw new NotValidPaymentException("Can not charge customer");
            }
        }

        /// <summary>
        /// AuthorizeCashPayment
        /// </summary>
        /// <param name="totalAmount"></param>
        /// <returns>Message</returns>
        private string  AuthorizeCashPayment(decimal totalAmount)
        {
            string Message = string.Empty;
            if (totalAmount > 20)
                throw new UnauthorizedCashPayment("Amount is too big");
            else
            {
                Message = string.Format("Payment for ${0} has been done", totalAmount);
                Logger.Info(Message);
            }
            return Message;
        }
    }
}
