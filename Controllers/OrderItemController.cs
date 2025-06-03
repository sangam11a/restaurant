using Microsoft.AspNetCore.Mvc;
using Stock.Data;
using Stock.Models.ModelDTO;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public OrderItemController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAllOrderItem() {
            var allData = dbContext.Orders.ToList();
            if(allData is null)
            {
                return NotFound();
            }
            return Ok(allData);
        }

        [HttpPost]
        public ActionResult AddOrderItem([FromBody] OrderItemDTO itemDto)
        {
            var newData = new OrderItemDTO {
                MenuId = itemDto.MenuId,
                Quantity = itemDto.Quantity,
                Price = itemDto.Price,
                OrderId = itemDto.OrderId,
            };
            if(newData is null)
            {
                return NotFound(new {message="The list is empty."});
            }
            return Ok(newData);
        }
    }
}
