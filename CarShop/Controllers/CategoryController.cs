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


        // Get-method для редактирования категории
        /// <summary>
        /// Этот метод вызывается при нажатии кнопки Edit
        /// Он определяет, какую именно категорию нужно редактировать
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult Edit(int? Id)  
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            // Метод Find работает только с первичным ключом. Здесь извлекается категория из базы данных
            Category? categoryFromDb = _db.Categories.Find(Id);
            // Ещё два способа извлечь категорию из базы данных
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id.Equals(Id));
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == Id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound(); 
            }
            return View(categoryFromDb);
        }

        // Post-method для редактирования категории
        [HttpPost]
        public IActionResult Edit(Category? obj)
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


        // Get-method для создания категории
        public IActionResult Create()
        {
            return View();
        }

        // Post-method для создания категории
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
