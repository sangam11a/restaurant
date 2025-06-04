using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using var transaction = dbContext.Database.BeginTransaction();

            try
            {
                var newFoodCategory = new FoodCategory
                {
                    CategoryName = categoryDto.CategoryName,
                };
                //var getFoodCategory = dbContext.FoodCategory.Find();
                if (newFoodCategory is null)
                {
                    return NotFound();
                }
                dbContext.FoodCategories.Add(newFoodCategory);
                dbContext.SaveChanges();
                transaction.Commit();
                return Ok(categoryDto);
            }
            catch
            {
                return BadRequest(new { message = "Some error occured while writing data" });
            }
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var getAllData = dbContext.FoodCategories.ToList();
            if(getAllData is null)
            {
                return NotFound(new { message="No data found"});
            }
            return Ok(getAllData);
        }
        [HttpDelete("id")]
        public IActionResult DeleteCategory(int id)
        {
            var dataExists = dbContext.FoodCategories.FirstOrDefault(o => o.FoodCategoryId == id);
            if(dataExists == null)
            {
                return NotFound();
            }
            dbContext.FoodCategories.Remove(dataExists);
            dbContext.SaveChanges();
            return Ok(dataExists);
        }
    }
}
