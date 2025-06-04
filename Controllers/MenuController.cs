using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Models;
using Stock.Models.ModelDTO;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public MenuController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllMenus()
        {
            var menus = dbContext.Menus
                .Include(m => m.Category)
                .ToList();

            return Ok(menus);
        }

        [HttpPost]
        public IActionResult AddMenu([FromBody] MenuDTO menuDto)
        {
            try
            {
                var newMenu = new Menu
                {
                    FoodName = menuDto.FoodName,
                    Price = menuDto.Price,
                    FoodCategoryId = menuDto.FoodCategoryId,
                };
                dbContext.Menus.Add(newMenu);
                dbContext.SaveChanges();
                return Ok(newMenu);
            }
            catch (Exception ex)
            {
                  return StatusCode(500, new { message = "An error occurred while adding the menu item.", error = ex.Message });
            }

        }
        [HttpGet("id:{int}")]
        public IActionResult GetUsingId(int id) {
            var menuItem = dbContext.Menus.FirstOrDefault(m => m.MenuId == id);

            if (menuItem == null)
            {
                return NotFound(new { message = $"Menu item with ID {id} not found." });
            }

            return Ok(menuItem);
        }
    }
}
