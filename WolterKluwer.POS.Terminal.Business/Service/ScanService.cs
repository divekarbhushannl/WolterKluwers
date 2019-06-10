using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class ScanService : IScan
    {
        /// <summary>
        /// Scan the Product
        /// </summary>
        /// <param name="availableProducts"></param>
        /// <param name="productName"></param>
        /// <returns>OrderItem</returns>
        public OrderItem Scan(List<Product> availableProducts, char productName)
        {
            OrderItem _orderItem;
                try
                {
                var product = availableProducts.Where(x => x.Name == productName.ToString()).FirstOrDefault();
                _orderItem = new OrderItem(product.Name, 0, product.Price);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                     _orderItem = null;
                }

            return _orderItem;
        }
    }
}
