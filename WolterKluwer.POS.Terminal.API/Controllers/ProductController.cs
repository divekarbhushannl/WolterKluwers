using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WolterKluwer.POS.Terminal.Business.Contract;
using WolterKluwer.POS.Terminal.Business.Service;
using WolterKluwer.POS.Terminal.Entities;

namespace WolterKluwer.POS.Terminal.API.Controllers
{
    public class ProductController : ApiController
    {
        public ProductController()
        {

        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="order"></param>
        /// <returns>return resonse which contains type of Order</returns>
        [HttpPost]
        [Route("api/Product")]
        public IHttpActionResult Post(Order order)
        {
            if (order == null ||order.Items == null || !order.Items.Any())
            {
                return BadRequest();
            }

            IPOSTerminalContract service = new POSTerminalService();
            Order result = service.CalculateTotalPrice( order);

            //if (result < 0)
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result); 
        }
    }
}
