using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.Business.Contract
{
    public interface IProductDiscount
    {
        List<ProductDiscount> GetProductDiscount();
    }
}
