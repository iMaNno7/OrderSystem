using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;
using WebApi.Infrastructure.Persistence;
using WebApi.Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OrdersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // return list orders
        //httpGet defult asp.net
        // api/orders
        [HttpGet(Name = nameof(GetAll))]
        public ActionResult GetAll()
        {
            return Ok(_dbContext.Orders.Include(s => s.OrderItems).ToList());
        }

        //api/orders/{orderId}
        [HttpGet("{orderId}", Name = nameof(GetById))]
        public ActionResult GetById([FromRoute] Guid orderId)
        {
            var order = _dbContext.Orders.Find(orderId);
            
            if (order is null)
                return NotFound();

            return Ok(order);
        }
        //Create
        //api/orders
        //request body
        [HttpPost]
        public ActionResult Create([FromBody] OrderDto order)
        {
            var orderEntity = new Order(order.Username,
                order.OrderItems.Select(item => new OrderItem(item.Price, item.Title, item.Count)).ToList());
            
            _dbContext.Orders.Add(orderEntity);
            
            _dbContext.SaveChanges();
            return Created(Url.Link(nameof(GetById), new { orderId = orderEntity.Id }),new { orderId=orderEntity.Id});
        }

    }
}
