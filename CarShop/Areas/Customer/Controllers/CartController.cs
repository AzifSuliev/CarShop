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

        public IActionResult Summary()
        {
            return View();
        }

        // прибавить товар (базовая комплектация++)
        public IActionResult PlusBasicEquipment(int cartId)
        {
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            shoppingCartFromDb.CountBasic++;
            _unitOfWork.ShoppingCart.Update(shoppingCartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        // отнять товар (базовая комплектация--)
        public IActionResult MinusBasicEquipment(int cartId)
        {
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if ((shoppingCartFromDb.CountBasic + shoppingCartFromDb.CountFull <= 1) && shoppingCartFromDb.CountBasic == 1)
            {
                _unitOfWork.ShoppingCart.Remove(shoppingCartFromDb);
            }
            else if (shoppingCartFromDb.CountBasic >= 1)
            {
                shoppingCartFromDb.CountBasic--;
                _unitOfWork.ShoppingCart.Update(shoppingCartFromDb);
            }
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
        // прибавить товар (полная комплектация ++)
        public IActionResult PlusFullEquipment(int cartId)
        {
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            shoppingCartFromDb.CountFull++;
            _unitOfWork.ShoppingCart.Update(shoppingCartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // отнять товар (полная комплектация --)
        public IActionResult MinusFullEquipment(int cartId)
        {
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if ((shoppingCartFromDb.CountFull + shoppingCartFromDb.CountBasic <= 1) && (shoppingCartFromDb.CountFull == 1))
            {
                _unitOfWork.ShoppingCart.Remove(shoppingCartFromDb);
            }
            else if(shoppingCartFromDb.CountFull >= 1)
            {
                shoppingCartFromDb.CountFull--;
                _unitOfWork.ShoppingCart.Update(shoppingCartFromDb);
            }
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveCart(int cartId)
        {
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(shoppingCartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
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
