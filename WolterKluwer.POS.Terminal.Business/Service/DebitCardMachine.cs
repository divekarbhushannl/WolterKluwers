using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Utilities;
using WolterKluwer.POS.Terminal.Utilities.Exceptions;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class DebitCardMachine : IDisposable
    {
        public string CardNumber { get; set; }
        public string ExpiresMonth { get; set; }
        public string ExpiresYear { get; set; }
        public string NameOnCard { get; set; }
        public decimal AmountToCharge { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void Charge()
        {
            ConnectToGateway("127.0.0.1", CardNumber, ExpiresMonth, ExpiresYear, NameOnCard);
            Charge(AmountToCharge);
        }

        /// <summary>
        /// Charge the amount
        /// </summary>
        /// <param name="amountToCharge"></param>
        /// <returns></returns>
       
        private void Charge(decimal amountToCharge)
        {
            //Charge amount
        }

        private void ConnectToGateway(string gatewayAddress, string cardNumber, string expiresMonth, string expiresYear, string nameOnCard)
        {
            try
            {
                //connect to gateway
            }
            catch (GatewayConnectionException gcException)
            {
                Logger.Error(gcException.Message, gcException);
                throw new OrderException("Can not connect to gateway", gcException);
            }
        }
    }
}
