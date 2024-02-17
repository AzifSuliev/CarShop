using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace CarShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Этот код нужен для извлечения идентификатора пользователя
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                includeCategoryProperties: "Product.Category",
                includeBrandProperties: "Product.Brand"
                )
            };

            int amountOfCars = 0;
            foreach (ShoppingCart cart in ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.OrderTotal += GetPriceBasedOnEquipment(cart);
                amountOfCars += cart.CountBasic + cart.CountFull;
            }

            // Если в корзине 2 и больше единиц товара, то скидка 5 %
            if (amountOfCars >= 2) ShoppingCartVM.OrderTotal *= 0.95;

            return View(ShoppingCartVM);
        }

        // Метод для вычисления суммы для каждой записи в корзину
        private double GetPriceBasedOnEquipment(ShoppingCart shoppingCart)
        {
            double totalPrice; // общая сумма записи для каждого Id
            totalPrice = shoppingCart.Product.basicEquipmentPrice * shoppingCart.CountBasic +
                shoppingCart.Product.fullEquipmentPrice * shoppingCart.CountFull;
            return totalPrice;
        }
    }
}
