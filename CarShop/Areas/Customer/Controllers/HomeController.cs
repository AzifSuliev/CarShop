using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using Microsoft.AspNetCore.Authorization;
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
                Count = 1,
                ProductId = productId
            };
            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            // ���� ��� ����� ��� ���������� ������������
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;


            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shoppingCart.ProductId);

            if(cartFromDb != null)
            {
                // ������� ����������
                cartFromDb.Count += shoppingCart.Count; // ���������� ���������� ������������ �������
                _unitOfWork.ShoppingCart.Update(cartFromDb); // ���������� ����� ���������� �������
            }
            else
            {
                // �������� �������
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            TempData["success"] = "������� ���� ������� ���������";
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index)); // ����� nameOf() �������������� �������� ������ Details()
                                                    // � �� �������������, ������� ������� � �������� ���������,
                                                    // � ������ ������ - ��� ������������� Index
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
