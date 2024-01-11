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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Upsert(int? Id) // = Update + Insert
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

            if(Id == null || Id == 0) // Если выражение истино, то это Create 
            {
                // create
                return View(productVm);
            }
            else // Иначе -- Update
            {
                // update
                productVm.Product = _unitOfWork.Product.Get(u => u.Id == Id);
                return View(productVm);
            }
        }


        /// <summary>
        /// Post метод Create
        /// </summary>
        /// <param name="productVm"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Upsert(ProductVM productVm, IFormFile? file)
        {
            if (ModelState.IsValid) // Проверка состояния модели
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productVm.Product.ImageURL) )
                    {
                        // Удаление изображения 
                        var oldImagePath = 
                            Path.Combine(wwwRootPath, productVm.Product.ImageURL.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVm.Product.ImageURL = @"\images\product\" + fileName;
                }

                if(productVm.Product.Id == 0) 
                {
                    // Добавление нового объекта при Id равным 0
                    _unitOfWork.Product.Add(productVm.Product);
                }
                else
                {
                    // Редактирование существующего объекта при Id НЕ равным 0
                    _unitOfWork.Product.Update(productVm.Product);
                }
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
