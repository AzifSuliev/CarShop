using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductsList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductsList);
        }

        /// <summary>
        /// Get метод Create
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IActionResult Create() 
        {
            return View();
        }


        /// <summary>
        /// Post метод Create
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid) // Проверка состояния модели
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Машина была успешно добавлена!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        /// <summary>
        /// Get метод Edit
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult Edit(int Id)
        {
            if (Id == null || Id == 0) return NotFound();

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == Id);
            if (productFromDb == null) return NotFound();
            return View(productFromDb);
        }

        /// <summary>
        /// Post метод Edit
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid) // Проверка состояния модели
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Машина была успешно изменена!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            // Здесь извлекается категория из базы данных
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == Id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        /// <summary>
        /// Post метод Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Машина была успешно удалена!";
            return RedirectToAction("Index", "Product");
        }
    }
}
