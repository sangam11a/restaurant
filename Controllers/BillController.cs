using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Models;
using Stock.Models.ModelDTO;

namespace Stock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public BillController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //    [HttpPost("pay")]
        //    public IActionResult GenerateBill([FromBody] BillDTO billDto)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        // 1. Get unpaid orders for the customer
        //        var unpaidOrders = dbContext.Orders
        //            .Where(o => o.CustomerId == billDto.CustomerId && o.Status == false)
        //            .ToList();

        //        if (!unpaidOrders.Any())
        //        {
        //            return NotFound("No unpaid orders found for this customer.");
        //        }

        //        // 2. Calculate total amount
        //        var totalAmount = unpaidOrders.Sum(o => o.Price);

        //        // 3. Mark orders as paid
        //        foreach (var order in unpaidOrders)
        //        {
        //            order.Status = true;
        //        }

        //        // 4. Create new bill
        //        var newBill = new Bill
        //        {
        //            Amount = (long)totalAmount,
        //            Status = true,
        //            PaymentTime = DateTime.Now,
        //            EmpId = billDto.EmpId,
        //            CustomerId = billDto.CustomerId
        //        };

        //        // 5. Save changes
        //        dbContext.Bills.Add(newBill);
        //        dbContext.SaveChanges();

        //        return Ok(newBill);
        //    }

        //    [HttpGet]
        //    public IActionResult GetBills()
        //    {
        //        var readData = dbContext.Bills.ToList();
        //        if(readData is null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(readData);
        //    }
        //}
        [HttpGet]
        public IActionResult GetAllBills()
        {
            var newData = dbContext.Bills.ToList();
            if(newData is null)
            {
                return NotFound(new { message = "Data not found" });
            }
            return Ok(newData);
        }

        [HttpPost("pay")]
        public IActionResult GenerateBill([FromBody] BillDTO billDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var transaction = dbContext.Database.BeginTransaction();
            try
            {
                // 1. Get unpaid orders for the customer
                var unpaidOrders = dbContext.Orders
                    .Where(o => o.CustomerId == billDto.CustomerId && !o.Status)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Menu) // If Menu contains the actual Price
                    .ToList();

                if (!unpaidOrders.Any())
                {
                    return NotFound("No unpaid orders found for this customer.");
                }

                var totalAmount = unpaidOrders
                    .SelectMany(o => o.OrderItems)
                    .Sum(oi => oi.Menu.Price * oi.Quantity);


                if (!unpaidOrders.Any())
                {
                    return NotFound("No unpaid orders found for this customer.");
                }

                // 2. Calculate total amount
                //float totalAmount = unpaidOrders.Sum(o => o.Price * o.quantity);

                // 3. Create the bill
                var newBill = new Bill
                {
                    Amount = (long)totalAmount,
                    Status = true,
                    PaymentTime = DateTime.Now,
                    EmpId = billDto.EmpId,
                    CustomerId = billDto.CustomerId
                };

                dbContext.Bills.Add(newBill);
                dbContext.SaveChanges(); // Save early to get Bill ID if needed

                // 4. Update orders as paid (optionally associate with bill if model allows)
                foreach (var order in unpaidOrders)
                {
                    order.Status = true;
                    // If Order has BillId FK in future: order.BillId = newBill.Id;
                }

                dbContext.SaveChanges();
                transaction.Commit();

                return Ok(new
                {
                    Bill = newBill,
                    OrdersPaid = unpaidOrders.Select(o => new
                    {
                        o.OrdId,
                        Items = o.OrderItems.Select(oi => new
                        {
                            oi.MenuId,
                            oi.Menu.FoodName,
                            oi.Menu.Price,
                            oi.Quantity
                        })
                    }),
                    Total = totalAmount
                });

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return StatusCode(500, "Payment failed. " + ex.Message);
            }
        }
        [HttpGet("id")]
        public IActionResult GetBillFromId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = dbContext.Bills.FirstOrDefault(o => o.BillId == id);
            if (data is null)
            {
                return NotFound(new { message = $"Bill with id {id} not found" });
            }
            return Ok(data);
        }

    }
   
    
}
