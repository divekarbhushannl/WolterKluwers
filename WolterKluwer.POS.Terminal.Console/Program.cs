using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Business.Service;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.Console
{
    class Program
    {
        private static POSTerminalService _iposTerminalService;
        
        private static Order _order;

        static void Main(string[] args)
        {
            System.Console.WriteLine("Point of Sale Terminal Application");
            System.Console.WriteLine("-------------------------------------");
            Initialize();
            GetAvailableProducts();
            ScanProductAndcalcuateTotalAmount();

        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private static void Initialize()
        {
            _iposTerminalService = new POSTerminalService();
        }

        /// <summary>
        /// GetAvailableProducts
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private static void GetAvailableProducts()
        {
            System.Console.WriteLine("Available Products");
            System.Console.WriteLine("-------------------------------------");
            
            var products = _iposTerminalService.GetAvailableProduct();
            var productdiscounts = _iposTerminalService.GetAvailableProductDiscount();

            foreach (var item in products)
            {
                var isDiscountAvailable = productdiscounts.Where(d => d.ProductId == item.Id).Any();
                if (isDiscountAvailable)
                {
                    var volume = productdiscounts.Where(d => d.ProductId == item.Id).FirstOrDefault().Volume;
                    var volumePrice = productdiscounts.Where(d => d.ProductId == item.Id).FirstOrDefault().VolumePrice;
                    System.Console.WriteLine($"Product Id : {item.Id} : Product Name : {item.Name} : Price per unit ${item.Price} : Discount ${volumePrice} for {volume} ");
                }
                else
                {
                    System.Console.WriteLine($"Product Id : {item.Id} : Product Name : {item.Name} : Price per unit ${item.Price} : No Discount ");
                }
                System.Console.WriteLine("------------------------------------------------");
            }

        }

        /// <summary>
        /// ScanProductAndcalcuateTotalAmount
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static void ScanProductAndcalcuateTotalAmount()
        {
            bool cont = true;
            string Message = string.Empty;
            do
            {
                var orderItem = Scan();
                if (orderItem.Count() > 0)
                {
                    _order = new Order(orderItem, 0);
                    var response = CalculateTotalPrice(_order);
                    System.Console.WriteLine();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {   
                        var resultorder = JsonConvert.DeserializeObject<Order>(response.Content.ReadAsStringAsync().Result);
                        System.Console.WriteLine($"\nProducts scanned successfully : Total Price for products ${resultorder.TotalAmount}");
                        System.Console.WriteLine($"\n\n   Receipt of the Order\n");
                        System.Console.WriteLine($"{ _iposTerminalService.PrintReceipt(resultorder).Body}");
                        _iposTerminalService.Payment(resultorder.TotalAmount, ref Message);
                        System.Console.WriteLine($"\n" + Message);
                    }
                    else
                    {
                        System.Console.WriteLine("\nError while calculating total price for products");
                    }
                }
                System.Console.WriteLine("\n\nIf you want to do shopping again then please enter Y");
                var input = System.Console.ReadKey();
                if (input.KeyChar.ToString().ToUpper() != "Y")
                {
                    cont = false;
                    break;
                }
            }
            while (cont);
        }

        /// <summary>
        /// Scan
        /// </summary>
        /// <param name=""></param>
        /// <returns>OrdetItems</returns>
        private static List<OrderItem> Scan()
        {
            bool cont = true;

            System.Console.WriteLine(" \n\nEnter product Name for scanning between above product (A,B,C,D) and press Enter to complete shopping");
            var orderItem = new List<OrderItem>();
            do
            {
                var input = System.Console.ReadKey();

                if (input.Key.Equals(ConsoleKey.Enter))
                {
                    cont = false;
                    break;
                }

                var isKeyParsed = Char.TryParse(input.KeyChar.ToString(), out char productName);

                if (!isKeyParsed)
                {
                    System.Console.WriteLine($"\nInvalid product Name entered as {input.KeyChar.ToString()}");
                }
                else
                {
                    var _orderItem = _iposTerminalService.ScanProduct(input.KeyChar);
                    if (_orderItem == null)
                    {
                        System.Console.WriteLine($"\nInvalid product Name entered as {input.KeyChar.ToString()}");
                        orderItem.Clear();
                        break;
                    }
                    else
                        orderItem.Add(_orderItem);
                }
            } while (cont);
            return GetOrderItem(orderItem);
        }

        /// <summary>
        /// GetOrderItem
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>List of distinctProductCode as type of OrdetItem</returns>
        private static List<OrderItem> GetOrderItem(List<OrderItem> orderItem)
        {
            var _orderItem = new List<OrderItem>();
            List<string> ProductCode = new List<string>();

            foreach (var item in orderItem)
            {
                ProductCode.Add(orderItem.Where(p => p.ProductCode == item.ProductCode).FirstOrDefault().ProductCode);
            }
            var distinctProductCode = ProductCode.Distinct().ToList();

            foreach (string item in distinctProductCode)
            {
                var qty = orderItem.Where(p => p.ProductCode == item).Count();
                var price = orderItem.Where(p => p.ProductCode == item).FirstOrDefault().Price;
                _orderItem.Add(new OrderItem(item, qty, price));
            }
            return _orderItem;
        }

        /// <summary>
        /// CalculateTotalPrice
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Response which contains type Order with Total Amount/returns>
        private static HttpResponseMessage CalculateTotalPrice(Order order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49197/");
                var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                var response = client.PostAsync("http://localhost:49197/api/Product", content).GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    System.Console.WriteLine($"Error while processing your request : Error Code {response.StatusCode}");
                }

                return response;
            }

        }
        
    }
}
