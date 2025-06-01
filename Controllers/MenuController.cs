using Microsoft.AspNetCore.Mvc;

namespace Stock.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
