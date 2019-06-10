using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class POSTerminalService : IPOSTerminalContract
    {
        private ICalculatorService _calculatorService;
        private ICreateProductDetails _createProductDetailsService;
        private IProductDiscount _productDiscountService;
        private IScan _scanService;
        private IReceiptService _receiptService;
        private IPaymentService _paymentService;
        private IPaymentProcessor _paymentProcessor;

        public POSTerminalService()
        {
            _calculatorService = new CalculatorService();
            _createProductDetailsService = new CreateProductDetailsService();
            _productDiscountService = new ProductDiscountService();
            _scanService = new ScanService();
            _receiptService = new ReceiptService();
            _paymentService = new PaymentService(_paymentProcessor);
        }

        /// <summary>
        /// CalculateTotalPrice
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Order</returns>
        public Order CalculateTotalPrice(Order order)
        {
            return _calculatorService.CalculateTotalAmount(GetAvailableProduct(), GetAvailableProductDiscount(), order);
        }

        /// <summary>
        /// GetAvailableProduct
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of Product</returns>
        public List<Product> GetAvailableProduct()
        {
            return _createProductDetailsService.GetProductPricing();
        }

        /// <summary>
        /// GetAvailableProductDiscount
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of ProductDiscount</returns>
        public List<ProductDiscount> GetAvailableProductDiscount()
        {
            return _productDiscountService.GetProductDiscount();
        }

        /// <summary>
        /// ScanProduct
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns>OrderItem</returns>
        public OrderItem ScanProduct(char productCode)
        {
            return _scanService.Scan(GetAvailableProduct(), productCode);
        }

        /// <summary>
        /// PrintReceipt
        /// </summary>
        /// <param name="_order"></param>
        /// <returns>Receipt</returns>
        public Receipt PrintReceipt(Order _order)
        {
            return _receiptService.PrintReceipt(_order);
        }

        /// <summary>
        /// Payment
        /// </summary>
        /// <param name="totalAmount"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public void  Payment(decimal totalAmount,ref string Message )
        {
            PaymentDetails paymentDetails = new PaymentDetails();
            paymentDetails.PaymentMethod = PaymentMethod.Cash;
            _paymentService.Charge(paymentDetails, totalAmount,ref Message);
        }

    }
}
