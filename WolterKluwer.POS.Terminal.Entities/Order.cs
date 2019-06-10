using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Entities
{
    public class Order
    {
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }

        public Order(List<OrderItem> _item, decimal totalAmount)
        {
            Items = _item;
            TotalAmount = totalAmount;
        }
    }
}
