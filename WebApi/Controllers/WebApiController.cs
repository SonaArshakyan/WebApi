using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;
using WebApi.ProductManager;

namespace WebApi.Controllers
{

    //[Authorize(Roles = "user")]
    public class WebApiController : ApiController
    {
        private DataAccessLayer dataAccessLayer;
        public WebApiController()
        {
            dataAccessLayer = new DataAccessLayer();
        }

        Dictionary<int, string> products = new Dictionary<int, string>
        {
            { 1, "product1" },
            { 2, "product2" },
            { 3, "product3" },
            { 4, "product4" },
        };

        [Route("api/get-product/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            await Task.FromResult(10);
            var product = dataAccessLayer.GetProduct(id);
            return Ok(product);
        }

    }
}
