using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BrandController : Controller
    {  
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // Создаём коллекцию, типизированную классом Brand. 
            // Затем присваиваем список категорий из базы данных
            List<Brand> objBrandList = _unitOfWork.Brand.GetAll().ToList();
            return View(objBrandList);
        }

        // Get-method для создания марки
        public IActionResult Create()
        {
            return View();
        }

        // Post-method для создания марки
        [HttpPost]
        public IActionResult Create(Brand? obj)
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
                _unitOfWork.Brand.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Марка была успешно добавлена!";
                return RedirectToAction("Index", "Brand");
            }
            return View();
        }

        // Get-method для редактирования марки
        /// <summary>
        /// Этот метод вызывается при нажатии кнопки Edit
        /// Он определяет, какую именно марку нужно редактировать
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Метод Find работает только с первичным ключом. Здесь извлекается марка из базы данных
            Brand? brandFromDb = _unitOfWork.Brand.Get(u => u.Id == Id);

            // Ещё два способа извлечь категорию из базы данных
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id.Equals(Id));
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == Id).FirstOrDefault();
            if (brandFromDb == null)
            {
                return NotFound();
            }
            return View(brandFromDb);
        }

        // Post-method для редактирования марки
        [HttpPost]
        public IActionResult Edit(Brand? obj)
        {
            if (ModelState.IsValid) // Проверка состояния модели
            {
                _unitOfWork.Brand.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Марка была успешно изменена!";
                return RedirectToAction("Index", "Brand");
            }
            return View();
        }

        // Get-method для удаления марки
        /// <summary>
        /// Этот метод вызывается при нажатии кнопки Удалить
        /// Он определяет, какую именно марку нужно удалить
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
            Brand? brandFromDb = _unitOfWork.Brand.Get(u => u.Id == Id);
            // Ещё два способа извлечь категорию из базы данных
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id.Equals(Id));
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == Id).FirstOrDefault();
            if (brandFromDb == null)
            {
                return NotFound();
            }
            return View(brandFromDb);
        }

        // Post-method для удаления марки
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Brand obj = _unitOfWork.Brand.Get(u => u.Id == Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Brand.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Марка была успешно удалена!";
            return RedirectToAction("Index", "Brand");
        }
    }
}
