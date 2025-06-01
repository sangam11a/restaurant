using Microsoft.AspNetCore.Mvc;
using Stock.Data;
using Stock.Models;
using Stock.Models.ModelDTO;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public FoodCategoryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult AddCategory(FoodCategoryDto categoryDto)
        {
            var newFoodCategory = new FoodCategory { 
                CategoryName = categoryDto.CategoryName,
            };
            //var getFoodCategory = dbContext.FoodCategory.Find();
            if(newFoodCategory is null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
