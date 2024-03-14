using CarShop.DataAccess.Data;
using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Создаём коллекцию, типизированную классом Category. 
            // Затем присваиваем список категорий из базы данных
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Категория была успешно добавлена!";
                return RedirectToAction("Index", "Category");
            }
            return View();
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
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Метод Find работает только с первичным ключом. Здесь извлекается категория из базы данных
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == Id);

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
            if (ModelState.IsValid) // Проверка состояния модели
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Категория была успешно изменена!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }


        // Get-method для удаления категории
        /// <summary>
        /// Этот метод вызывается при нажатии кнопки Удалить
        /// Он определяет, какую именно категорию нужно удалить
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            // Метод Find работает только с первичным ключом. Здесь извлекается категория из базы данных
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == Id);
            // Ещё два способа извлечь категорию из базы данных
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id.Equals(Id));
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == Id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        // Post-method для удаления категории
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Category obj = _unitOfWork.Category.Get(u => u.Id == Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Категория была успешно удалена!";
            return RedirectToAction("Index", "Category");
        }
    }
}
