using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // localhost:5001/api/orders
        [HttpGet]
        [Logger]
        public ActionResult GetAll()
        {
            // get all order
            return Ok();
        }
        
        // localhost:5001/api/orders/username
        [HttpGet("{username}")]
        public ActionResult Get([FromRoute] string username)
        {
            // get order
            return Ok();
        }
        

        // localhost:5001/api/orders
        [HttpPost]
        public ActionResult Create([FromRoute] string username)
        {
            // Create
            return Ok();
        }

        // localhost:5001/api/orders/UserName
        [HttpPut("{username}")]
        public ActionResult Update([FromRoute] string username)
        {
            // Create
            return Ok();
        }


    }
}
