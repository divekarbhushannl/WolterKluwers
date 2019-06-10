using System;
using System.Collections.Generic;
using System.Text;

namespace WolterKluwer.POS.Terminal.Entities
{
    public class ProductDiscount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Volume { get; set; }
        public decimal VolumePrice { get; set; }
    }
}
