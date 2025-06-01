using Microsoft.AspNetCore.Mvc;
using Stock.Data;
using Stock.Models;
using Stock.Models.ModelDTO;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CustomerController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerDTO customerDto)
        {
            var newCustomer = new Customer
            {
                CustomerName = customerDto.CustomerName,
                Address = customerDto.Address,
                Date = customerDto.Date,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Type = customerDto.Type,    
                TableNumber= customerDto.TableNumber,
                TimeIn = TimeOnly.FromDateTime(DateTime.Now),
            };
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();

            return Ok();
        }
        [HttpGet]
        public IActionResult GetCustomer()
        {
            var dataCustomer = dbContext.Customers.ToList();
            if(dataCustomer is null)
            {
                return NotFound();
            }
            return Ok(dataCustomer);
        }
    }
}
