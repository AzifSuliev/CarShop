using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

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
            ProductVM productVm = new ProductVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                BrandList = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            // ViewBag.CategoryList = CategoryList; // Динамический контейнер ViewBag
            // ViewData["CategoryList"] = CategoryList; // Динамический контейнер ViewData
            return View(productVm);
        }


        /// <summary>
        /// Post метод Create
        /// </summary>
        /// <param name="productVm"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProductVM productVm)
        {
            if (ModelState.IsValid) // Проверка состояния модели
            {
                _unitOfWork.Product.Add(productVm.Product);
                _unitOfWork.Save();
                TempData["success"] = "Машина была успешно добавлена!";
                return RedirectToAction("Index", "Product");
            }
            else
            {

                productVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                productVm.BrandList = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(productVm);

            }   
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

            ProductVM productVM = new ProductVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                BrandList = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Product = productFromDb
            };
            
            if (productVM == null) return NotFound();
            return View(productVM);
        }

        /// <summary>
        /// Post метод Edit
        /// </summary>
        /// <param name="productVm"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(ProductVM productVm)
        {
            if (ModelState.IsValid) // Проверка состояния модели
            {
                _unitOfWork.Product.Update(productVm.Product);
                _unitOfWork.Save();
                TempData["success"] = "Машина была успешно изменена!";
                return RedirectToAction("Index", "Product");
            }
            else
            {

                productVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                productVm.BrandList = _unitOfWork.Brand.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(productVm);

            }
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
