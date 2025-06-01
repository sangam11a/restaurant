using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Models;
using Stock.Models.ModelDTO;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public StoreController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult AddStore(StoreDTO storeDto)
        {
            var newStoreData = new Store{ 
            Name = storeDto.Name,
            Status = storeDto.Status,
            };
            try
            {
                dbContext.Stores.Add(newStoreData);
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log exception (optional: inject ILogger<ControllerName>)
                return StatusCode(500, new { message = "Failed to add store.", error = ex.Message });
            }
            return Ok(storeDto);
        }
        [HttpGet]
        public IActionResult GetStoreData()
        {
            var storeData = dbContext.Stores.ToList();
            if (storeData is null)
            {
                return NotFound();
            }
            return Ok(storeData);
        }
    }
}
