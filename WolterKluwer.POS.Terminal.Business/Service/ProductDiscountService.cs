using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class ProductDiscountService : IProductDiscount
    {
        /// <summary>
        /// GetProductDiscount
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of ProductDiscount</returns>
        public List<ProductDiscount> GetProductDiscount()
        {
            var productDiscount = new List<ProductDiscount>();
            productDiscount.Add(new ProductDiscount { Id = 1, ProductId = 1, Volume = 3, VolumePrice = 3.00M });
            productDiscount.Add(new ProductDiscount { Id = 2, ProductId = 3, Volume = 6, VolumePrice = 5.00M });
            return productDiscount;
        }
    }
}
