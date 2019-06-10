using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using AutoFixture;
using NUnit.Framework;
using System.Linq;
using System.Web.Http;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Business.Service;
using WolterKluwer.POS.Terminal.Entities;
using WolterKluwer.POS.Terminal.API.Controllers;
using System.Web.Http.Results;

namespace WolterKluwer.POS.Terminal.Test
{
    [TestFixture]
    public class WolterKluwerPOSTerminalTest
    {

        POSTerminalService _posTerminalSerivce;
        ProductController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new ProductController();
            _posTerminalSerivce = new POSTerminalService();
        }

        /// <summary>
        /// CreatOrder
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns>Order type</returns>
        public Order CreatOrder(List<char> ProductName)
        {
            Order _order;

            var orderItem = CreateOrderItem(ProductName);
            if (orderItem !=null)
                _order = new Order(orderItem, 0);
            else
                _order = null;

            return _order;
        }

        /// <summary>
        /// CreateOrderItem
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns>OrderItems </returns>
        /// 
        public List<OrderItem>CreateOrderItem(List<char> ProductName)
        {
            var orderItem = new List<OrderItem>();
            
            //Scan Product code and check whether it is vailable in the database.
            foreach (char productCode in ProductName)
            {
                var _orderItem = _posTerminalSerivce.ScanProduct(productCode);
                if (_orderItem != null)
                    orderItem.Add(_orderItem);
                else
                    orderItem = null;
            }

            var oItem = new List<OrderItem>();

            if (orderItem != null)
            {
                //Findout distnct Product Code 
                var distinctProductCode = ProductName.Distinct().ToList();
                foreach (char item in distinctProductCode)
                {
                    var qty = orderItem.Where(p => p.ProductCode == item.ToString()).Count();
                    var price = orderItem.Where(p => p.ProductCode == item.ToString()).FirstOrDefault().Price;
                    oItem.Add(new OrderItem(item.ToString(), qty, price));
                }
            }
            else
                oItem = null;

            return oItem;
        }


        /// <summary>
        /// Should_return_725M_when_ProductOrder_is_ABCD
        /// </summary>
        /// <param name=""></param>
        /// <returns>7.25M </returns>
        /// 
        [TestCase]
        public void Should_return_725M_when_ProductOrder_is_ABCD()
        {
            Order _order;
           
            //When 
            var productNamesSet1 = new List<char> {
                'A','B','C','D'
            };

            // Act
            _order = CreatOrder(productNamesSet1);
            
            var result1 = _controller.Post(_order) as OkNegotiatedContentResult<Order>;

            // Assert
            Assert.IsNotNull(result1);
            Assert.AreEqual(7.25M, result1.Content.TotalAmount);
        }

        /// <summary>
        /// Should_return_1325M_when_ProductOrder_is_ABCDABA
        /// </summary>
        /// <param name=""></param>
        /// <returns>13.25M </returns>
        /// 
        [TestCase]
        public void Should_return_1325M_when_ProductOrder_is_ABCDABA()
        {
            Order _order;
            
            //When 
            var productNamesSet2 = new List<char> {
                'A','B','C','D','A','B','A'
            };

            // Act
            _order = CreatOrder(productNamesSet2);
            var result2 = _controller.Post(_order) as OkNegotiatedContentResult<Order>;

            // Assert
            Assert.IsNotNull(result2);
            Assert.AreEqual(13.25M, result2.Content.TotalAmount);
        }

        /// <summary>
        /// Should_return_6M_when_ProductOrder_is_CCCCCCC
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns>6.00M </returns>
        /// 
        [TestCase]
        public void Should_return_6M_when_ProductOrder_is_CCCCCCC()
        {
            Order _order;
           
            //When 
            var productNamesSet3 = new List<char> {
                'C','C','C','C','C','C','C'
            };

            // Act
            
            _order = CreatOrder(productNamesSet3);
            var result3 = _controller.Post(_order) as OkNegotiatedContentResult<Order>;

            // Assert
            Assert.IsNotNull(result3);
            Assert.AreEqual(6.00M, result3.Content.TotalAmount);
        }

        /// <summary>
        /// Should_return_null_when_ProductOrder_is_InvalidProductCode
        /// </summary>
        /// <param name=""></param>
        /// <returns>NULL </returns>
        /// 
        [TestCase]
        public void Should_return_null_when_ProductOrder_is_InvalidProductCode()
        {
            Order _order;
            
            //When 
            var productNamesSet4 = new List<char> {
                'E'
            };

            // Act
          
            _order = CreatOrder(productNamesSet4);
            var result4= _controller.Post(_order) as OkNegotiatedContentResult<Order>;

            // Assert
            Assert.IsNull(result4);
        }
    }
}
