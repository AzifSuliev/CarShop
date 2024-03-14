using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CarShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {           

            IEnumerable<Product> productList = _unitOfWork.Product.
                GetAll(includeCategoryProperties: "Category", includeBrandProperties: "Brand");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart shoppingCart = new()
            {
                Product = _unitOfWork.Product.
                Get(u => u.Id == productId, includeCategoryProperties: "Category",
                includeBrandProperties: "Brand"),
                CountBasic = 0,
                CountFull = 0,
                ProductId = productId
            };
            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            // Этот код нужен для извлечения пользователя
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;


            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shoppingCart.ProductId);

            if(cartFromDb != null)
            {
                // корзина существует
                cartFromDb.CountBasic += shoppingCart.CountBasic; // обновление количества существующей корзины
                cartFromDb.CountFull += shoppingCart.CountFull;
                _unitOfWork.ShoppingCart.Update(cartFromDb); // обновление всего экземпляра корзины
                _unitOfWork.Save();
            }
            else
            {
                // добавить запись для корзины
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }
            TempData["success"] = "Корзина была успешно обновлена";
            return RedirectToAction(nameof(Index)); // метод nameOf() перенаправляет резуьтат метода Details()
                                                    // в то представление, которое указано в качестве аргумента,
                                                    // в данном случае - это представление Index
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
