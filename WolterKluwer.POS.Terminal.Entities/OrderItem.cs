using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Entities
{
    public class OrderItem
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }


        public OrderItem (string productCode,int qty,decimal price)
        {
            ProductCode = productCode;
            Quantity = qty;
            Price = price;
        }
    }
}
