using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
