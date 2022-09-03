using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using WebApp.Filter;
using WebApp.Infrastructure.Persistence;
using WebApp.Models;

namespace Controllers;
[ApiController]
[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public OrdersController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //api/orders
    [HttpGet(Name = nameof(GetAll))]
    public ActionResult<ApiResult> GetAll()
    {
        _dbContext.Orders.Add(new("iman", new() { new(1000, "chips", 2) }));
        _dbContext.SaveChanges();
        var orders = _dbContext.Orders.Include(s => s.OrderItems).ToList();
        return new ApiResult(Url.Link(nameof(GetAll), null), orders.Select(s => new
        {
            Id = s.Id,
            Username = s.Username,
            Address = s.Address,
            TrackingCode = s.TrackingCode,
            Href = Url.Link(nameof(GetById), new { orderId = s.Id }),
            OrderItems = s.OrderItems
        }));
    }

    [HttpGet("{orderId}", Name = "GetById")]
    public ActionResult<ApiResult> GetById([FromRoute] Guid orderId)
    {
        var order = _dbContext.Orders.SingleOrDefault(s => s.Id == orderId);
        if (order is null) return NotFound();

        return Ok(new ApiResult(Url.Link(nameof(GetById), null), order));
    }


}