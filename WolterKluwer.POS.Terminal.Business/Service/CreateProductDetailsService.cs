using System;
using System.Collections.Generic;
using System.Text;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class CreateProductDetailsService : ICreateProductDetails
    {
        /// <summary>
        /// CalculateTotalAmountwithDiscount
        /// </summary>
        /// <returns>List Of Product</returns>
        public List<Product> GetProductPricing()
        {
            var products = new List<Product>();
            products.Add(new Product { Id = 1, Name = "A", Price = 1.25M });
            products.Add(new Product { Id = 2, Name = "B", Price = 4.25M });
            products.Add(new Product { Id = 3, Name = "C", Price = 1.00M });
            products.Add(new Product { Id = 4, Name = "D", Price = 0.75M });
            return products;
        }
    }
}
