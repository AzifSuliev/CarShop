using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Models.ViewModels;
using CarShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace CarShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
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
                ),
                OrderHeader = new()
            };

            int amountOfCars = 0;
            foreach (ShoppingCart cart in ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += GetPriceBasedOnEquipment(cart);
                amountOfCars += cart.CountBasic + cart.CountFull;
            }


            // Если в корзине 2 и больше единиц товара, то скидка 5 %
            if (amountOfCars >= 2) ShoppingCartVM.OrderHeader.OrderTotal *= 0.95;

            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            // Этот код нужен для извлечения идентификатора пользователя
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                includeCategoryProperties: "Product.Category",
                includeBrandProperties: "Product.Brand"
                ),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddres;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
            int amountOfCars = 0;
            foreach (ShoppingCart cart in ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += GetPriceBasedOnEquipment(cart);
                amountOfCars += cart.CountBasic + cart.CountFull;
            }

            // Если в корзине 2 и больше единиц товара, то скидка 5 %
            if (amountOfCars >= 2) ShoppingCartVM.OrderHeader.OrderTotal *= 0.95;

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            // Этот код нужен для извлечения идентификатора пользователя
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                includeCategoryProperties: "Product.Category",
                includeBrandProperties: "Product.Brand");

            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            int amountOfCars = 0;
            foreach (ShoppingCart cart in ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += GetPriceBasedOnEquipment(cart);
                amountOfCars += cart.CountBasic + cart.CountFull;
            }

            // Если в корзине 2 и больше единиц товара, то скидка 5 %
            double discount = 0.95;
            if (amountOfCars >= 2) ShoppingCartVM.OrderHeader.OrderTotal *= discount;

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                // если условие истинно, то этот аккаунт является аккаунтом покупателя,
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending; // Статус оплаты в ожидании
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending; // Статус заказа в ожидании
            }
            else
            {
                // это компания
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApproved; // Статус оплаты подтверждён 
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved; // Статус заказа подтверждён
            }

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader); // Добавление в базу данных заказа
            _unitOfWork.Save(); // Сохранение базы данных

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    CountBasic = cart.CountBasic,
                    CountFull = cart.CountFull,
                    BasicEquipmentPrice = cart.BasicEquipmentPrice,
                    FullEquipmentPrice = cart.FullEquipmentPrice
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                // если условие истинно, то этот аккаунт является аккаунтом покупателя,
                // в таком случае мы должны потребовать оплату
                // логика Stripe

                StripeConfiguration.ApiKey = "sk_test_51OoQctFmqGqHHR4uCCp7XjDux9y0Qm55nzaI4q91OEW4yJuPha6jsL2HLZY0uy4GLyYkXzCFuVdvigYKzNb6l90Y00882tKj9e";

                var domain = "https://localhost:7029/";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?Id={ShoppingCartVM.OrderHeader.Id}",
                    CancelUrl = domain + "customer/cart/index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string>
                    {
                        "card" // Замените на нужные вам методы оплаты
                    }
                };

                if (amountOfCars >= 2) // количество товара >= 2 -- есть скидка
                {
                    foreach (var item in ShoppingCartVM.ShoppingCartList)
                    {
                        double exchangeRate = 92.05;
                        var sessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)((item.Product.basicEquipmentPrice * item.CountBasic * 100 + item.Product.fullEquipmentPrice * item.CountFull * 100) * discount / exchangeRate),
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Brand.Name + " " + item.Product.CarName
                                }
                            },
                            Quantity = item.CountBasic + item.CountFull
                        };
                        options.LineItems.Add(sessionLineItem);
                    }
                }
                else // скидки нет 
                {
                    foreach (var item in ShoppingCartVM.ShoppingCartList)
                    {
                        double exchangeRate = 92.05;
                        var sessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)((item.Product.basicEquipmentPrice * item.CountBasic * 100 + item.Product.fullEquipmentPrice * item.CountFull * 100) / exchangeRate),
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Brand.Name + " " + item.Product.CarName
                                }
                            },
                            Quantity = item.CountBasic + item.CountFull
                        };
                        options.LineItems.Add(sessionLineItem);
                    }
                }
                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.OrderHeader.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }

            return RedirectToAction(nameof(OrderConfirmation), new { Id = ShoppingCartVM.OrderHeader.Id });
        }

        public IActionResult OrderConfirmation(int Id)
        {
            return View(Id);
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
            else if (shoppingCartFromDb.CountFull >= 1)
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
