using Microsoft.AspNetCore.Mvc;
using Stock.Data;
using Stock.Models;
using Stock.Models.ModelDTO;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public OrderController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("orders")]
    public IActionResult GetAllOrders()
    {
        var allOrders = dbContext.Orders.ToList();
        return Ok(allOrders);
    }

    [HttpPost("order")]
    public IActionResult PostOrder(OrderDTO dto)
    {
        var newOrder = new Order
        {
            OrdId = dto.OrdId ?? 0,
            
            CustomerId = dto.CustomerId,
            Status = dto.Status
        };

        dbContext.Orders.Add(newOrder);
        dbContext.SaveChanges(); // ✅ Save to DB

        return Ok(newOrder);
    }
}
