using Microsoft.AspNetCore.Mvc;
using Stock.Data;
using Stock.Models.ModelDTO;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        public readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult PostEmployee(EmployeeDTO dto)
        {
            var newEmployee = new Employee()
            {
                Name = dto.Name,
                Gender = dto.Gender,
                Shift = dto.Shift,
                Salary = dto.Salary,
                DOB = dto.DOB,
                Address = dto.Address,
                Designation = dto.Designation,
                Phone = dto.Phone
            };
            dbContext.Add(newEmployee);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetEmployee()
        {
            var dataRead = dbContext.Employees.ToList();
            if(dataRead == null)
            {
                return NotFound();
            }
            return Ok(dataRead);    
        }
        [HttpGet("{id:Guid}")]
        public IActionResult GetEmployeeId(Guid id) {
            var employee = dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
