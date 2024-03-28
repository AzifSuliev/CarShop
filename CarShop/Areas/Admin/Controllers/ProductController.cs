using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Models.ViewModels;
using CarShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
            List<Product> objProductsList = _unitOfWork.Product.GetAll(includeCategoryProperties:"Category", 
                includeBrandProperties: "Brand").ToList();
            return View(objProductsList);
        }

        /// <summary>
        /// Get метод Upsert
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
                productVm.Product = _unitOfWork.Product.Get(u => u.Id == Id, includeProperties: "ProductImages");
                return View(productVm);
            }
        }


        /// <summary>
        /// Post метод Upsert
        /// </summary>
        /// <param name="productVm"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Upsert(ProductVM productVm, List<IFormFile>? files)
        {
            if (ModelState.IsValid) // Проверка валидации модели
            {
                if (productVm.Product.Id == 0)  // Добавление нового объекта при Id равным 0
                {
                    productVm.Product.Description = System.Text.RegularExpressions.Regex.Replace(productVm.Product.Description, "<.*?>", string.Empty);
                    _unitOfWork.Product.Add(productVm.Product);
                }
                else                           // Редактирование существующего объекта при Id НЕ равным 0
                {
                    productVm.Product.Description = System.Text.RegularExpressions.Regex.Replace(productVm.Product.Description, "<.*?>", string.Empty);
                    _unitOfWork.Product.Update(productVm.Product);
                }
                _unitOfWork.Save();
                // WebRootPath предназначен для предоставления физического пути к папке wwwroot
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                // переменная file - файл загруженный из страницы пользователя
                if (files != null)
                {

                    foreach(IFormFile file in files)
                    {
                        // Генерируется уникальное имя файла с использованием Guid.NewGuid()
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        /*
                          Path.Combine(wwwRootPath, @"images\product"): Это использует метод Path.Combine для объединения двух частей пути.
                          Первая часть — wwwRootPath, это физический путь к папке wwwroot. 
                          Вторая часть — @"images\product", это относительный путь, где images — подпапка в wwwroot, а product — подпапка внутри images.
    s                     string productPath - это переменная, которая получает финальный путь к папке, где будут храниться изображения продуктов. 
                          В результате выполнения этой строки, productPath будет содержать физический путь к папке wwwroot\images\product.
                         */
                        string productPath = @"images\products\product-" + productVm.Product.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);

                        if(!Directory.Exists(finalPath)) Directory.CreateDirectory(finalPath); // создание папки по указанному пути finalPath, если такой папки не существвет

                        // в этой части кода файл загружается на сервер и сохраняется в указанной директории,
                        // а затем путь к этому файлу присваивается свойству ImageURL объекта Product                                                                       
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        ProductImage productImage = new()
                        {
                            ImageUrl = @"\" + productPath + @"\" + fileName,
                            ProductId = productVm.Product.Id
                        };

                        if (productVm.Product.ProductImages == null) productVm.Product.ProductImages = new List<ProductImage>();
                        productVm.Product.ProductImages.Add(productImage);
                        _unitOfWork.ProductImage.Add(productImage);

                    }

                    _unitOfWork.Product.Update(productVm.Product);
                    _unitOfWork.Save();
                }

               
                TempData["success"] = "Выполнено!";
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

        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductsList = _unitOfWork.Product.GetAll(includeCategoryProperties: "Category",
               includeBrandProperties: "Brand").ToList();
            return Json(new {data = objProductsList });
        }

       // [HttpDelete]
        public IActionResult Delete(int? Id)
        {

            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == Id);

            if(productToBeDeleted == null)
            {
                return Json( new {success = false, message="Ошибка удаления"});
            }

           // var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageURL.TrimStart('\\'));
            //if (System.IO.File.Exists(oldImagePath))
            //{
            //    System.IO.File.Delete(oldImagePath);
            //}

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

           
            return Json(new { success = true, message = "Объект удалён" });
        }
        #endregion
    }
}
