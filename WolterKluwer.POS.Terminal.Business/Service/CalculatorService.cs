using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.Business.Service
{
    public class CalculatorService : ICalculatorService
    {
        /// <summary>
        /// CalculateTotalAmount
        /// </summary>
        /// <param name="availableProducts"></param>
        /// <param name="availableDiscount"></param>
        /// <param name="order"></param>
        /// <returns>order</returns>
        public Order CalculateTotalAmount(List<Product> availableProducts, List<ProductDiscount> availableDiscounts, Order order)
        {
            if (order == null || order.Items == null || !order.Items.Any())
            {
                return null;
            }

            decimal totalPrice = 0;
            
            foreach (var item in order.Items)
            {
                if (item == null)  //return -1
                    return null;

                var productId = availableProducts.Where(p => p.Name == item.ProductCode).FirstOrDefault().Id;
                var isDiscountAvailable = availableDiscounts.Where(d => d.ProductId == productId).Any();

                if (isDiscountAvailable)
                {
                    totalPrice +=  CalculateTotalAmountwithDiscount(availableDiscounts,productId, item);
                }
                else
                {
                    totalPrice += item.Quantity * item.Price;
                }
                
                order.TotalAmount = totalPrice;
            }
            return order;
        }

        /// <summary>
        /// CalculateTotalAmountwithDiscount
        /// </summary>
        /// <param name="availableDiscounts"></param>
        /// <param name="productId"></param>
        /// <param name="OrderItem"></param>
        /// <returns>totalPrice</returns>
        public decimal CalculateTotalAmountwithDiscount(List<ProductDiscount> availableDiscounts,int productId,OrderItem item)
        {
            decimal totalPrice = 0;
            var volume = availableDiscounts.Where(d => d.ProductId == productId).FirstOrDefault().Volume;
            var volumePrice = availableDiscounts.Where(d => d.ProductId == productId).FirstOrDefault().VolumePrice;

            if (item.Quantity == volume)
            {
                totalPrice += volumePrice;
            }

            if (item.Quantity > volume)
            {
                var cnt = item.Quantity / volume;

                var remainder = item.Quantity % volume;

                totalPrice += cnt * volumePrice;
                totalPrice += remainder * item.Price;

            }

            if (item.Quantity < volume)
            {
                totalPrice += item.Quantity * item.Price;
            }
            return totalPrice;
        }
    }

         
}
