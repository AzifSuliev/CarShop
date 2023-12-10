using CarShop.Data;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // Создаём коллекцию, типизированную классом Category. 
            // Затем присваиваем список категорий из базы данных
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        // Get-method
        public IActionResult Create()
        {
            return View();
        }

        // Post-method
        [HttpPost]
        public IActionResult Create(Category? obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) // Проверка на соответствие полей Name и DisplayOrder
            {
                // Ошибка
                ModelState.AddModelError("name", "The display order cannot exactly match the Name");
            }

            //if (obj.Name != null && obj.Name.ToLower().Equals("test") || (obj.Name != null && obj.Name.ToLower().Equals("тест")))// Проверка на соответствие полей Name и DisplayOrder
            //{
            //    // Ошибка
            //    ModelState.AddModelError("", "Test (тест) is the invalid value!");
            //}
            if (ModelState.IsValid) // Проверка состояния модели
                {
                    _db.Categories.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Category");
                }
            return View();
        }
    }
}
