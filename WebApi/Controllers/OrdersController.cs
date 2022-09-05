using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;
using WebApi.Infrastructure.persistence;
using WebApi.Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OrdersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // get all orders
        // api/orders
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public ActionResult GetAll()
        {
            var ordrs = _dbContext.Orders.Include(s=>s.OrderItems).ToList();
            var result = ordrs.Select(order => new
            {
                Href = Url.Link(nameof(GetById), new { orderId=order.Id}),
                Username = order.Username,
                OrderItems = order.OrderItems
            }).ToList();
            return Ok(result);
        }

        
        //get by Id
        [HttpGet("{orderId}",Name =nameof(GetById))]
        public ActionResult GetById([FromRoute]Guid orderId)
        {
            var order = _dbContext.Orders.Find(orderId);
            return Ok(order);
        }


        //create order
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesDefaultResponseType]
        //public ActionResult Create([FromBody]OrderDto dto)
        //{
            
        //    var order = new Order(dto.Username,
        //        dto.OrderItems.Select(item=>new OrderItem (item.Price, item.Title, item.Count)).ToList()
        //        ,new());
        //    _dbContext.Orders.Add(order);
        //    _dbContext.SaveChanges();

        //    return CreatedAtAction(nameof(GetById), new {orderId=order.Id },order);
        //}
    }
}
