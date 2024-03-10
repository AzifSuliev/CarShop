using CarShop.DataAccess.Repository.IRepository;
using CarShop.Models;
using CarShop.Models.ViewModels;
using CarShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using System.Security.Claims;

namespace CarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            OrderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product.Brand")
            };
            return View(OrderVM);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)] // Метод требует авторизации админа или работника
        public IActionResult UpdateOrderDetail()
        {
            // Идентификатор OrderVM.OrderHeader.Id извлекается благодаря этой записи --> <input asp-for="OrderHeader.Id" hidden />
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;
            orderHeaderFromDb.State = OrderVM.OrderHeader.State;
            orderHeaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;

            if (!string.IsNullOrEmpty(orderHeaderFromDb.Carrier)) orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            if (!string.IsNullOrEmpty(orderHeaderFromDb.TrackingNumber)) orderHeaderFromDb.Carrier = OrderVM.OrderHeader.TrackingNumber;

            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Детали заказа были обновлены успешно!";
            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)] // Метод требует авторизации админа или работника
        public IActionResult StartProcessing()
        {
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusInProcess);
            _unitOfWork.Save();
            TempData["Success"] = "Детали заказа были обновлены успешно!";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)] // Метод требует авторизации админа или работника
        public IActionResult ShipOrder()
        {
            var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeader.Carrier = OrderVM.OrderHeader.Carrier;
            orderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            orderHeader.OrderStatus = SD.StatusShipped;
            orderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
            orderHeader.ShippingDate = DateTime.Now;

            if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
            {
                // если оплата отложена, то к дате выгрузки прибавляем 30 дней
                orderHeader.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }

            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();
            TempData["Success"] = "Заказ был успешно выгружен!";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        /// <summary>
        /// метод для отмены заказа
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult CancelOrder()
        {

            var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);

            if (orderHeader.PaymentStatus == SD.PaymentStatusApproved)
            {
                // если заказ оплачен, то нужно делать возврат средств
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntendId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);
                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusRefunded);
            }
            else
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusCancelled);
            }
            _unitOfWork.Save();
            TempData["Success"] = "Заказ отменён!";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }


        /// <summary>
        /// метод для оплаты отложенного заказа
        /// </summary>
        /// <returns></returns>
        [ActionName("Details")]
        [HttpPost]
        public IActionResult Details_PAY_NOW()
        {
            OrderVM.OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id, includeProperties: "ApplicationUser");
            OrderVM.OrderDetails = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == OrderVM.OrderHeader.Id, includeProperties: "Product.Brand");
            StripeConfiguration.ApiKey = "sk_test_51OoQctFmqGqHHR4uCCp7XjDux9y0Qm55nzaI4q91OEW4yJuPha6jsL2HLZY0uy4GLyYkXzCFuVdvigYKzNb6l90Y00882tKj9e";
            // stripe logic
            // var domain = "https://localhost:7029/";
            var domain = Request.Scheme + "://" + Request.Host.Value + "/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"admin/order/PaymentConfirmation?orderHeaderId={OrderVM.OrderHeader.Id}",
                CancelUrl = domain + $"admin/order/details?orderId={OrderVM.OrderHeader.Id}",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                PaymentMethodTypes = new List<string>
                {
                   "card" // Замените на нужные вам методы оплаты
                }
            };
            int amountOfCars = 0;
            foreach (var item in OrderVM.OrderDetails) amountOfCars += item.CountBasic + item.CountFull;          
            double discount = 0.95;
            if (amountOfCars >= 2)
            {
                foreach (var item in OrderVM.OrderDetails)
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
                                Name = $"{item.Product.Brand.Name} {item.Product.CarName}"
                            }
                        },
                        Quantity =  1
                    };
                    options.LineItems.Add(sessionLineItem);
                }
            }
            else
            {
                foreach (var item in OrderVM.OrderDetails)
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
                                Name = $"{item.Product.Brand.Name} {item.Product.CarName}"
                            }
                        },
                        Quantity = 1
                    };
                    options.LineItems.Add(sessionLineItem);
                }
            }
            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStripePaymentId(OrderVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult PaymentConfirmation(int orderHeaderId) // Подтверждение заказа
        {
            // Извлечение объекта orderHeader из БД
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderHeaderId, includeProperties: "ApplicationUser");

            if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
            {                                                                            
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                if (session.PaymentStatus.ToLower() == "paid") // ключевое слово "paid" относится к Stripe
                                                               // если статус оплаты объекта session соответствует "paid",
                                                               // то происходит обновление статуса
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentId(orderHeaderId, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(orderHeaderId, orderHeader.OrderStatus, SD.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
            }
            return View(orderHeaderId);
        }


        #region APICALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaderList;

            // Если пользователь Админ или работник, то списку objOrderHeaderList присваиваются значения всех заказов
            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                objOrderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else // Иначе списку присваиваются значения тех заказов, которые принадлежат текщему пользователю
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                objOrderHeaderList = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == userId, includeProperties: "ApplicationUser");

            }

            switch (status)
            {
                case "paymentPending":
                    objOrderHeaderList = objOrderHeaderList.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
                    break;
                case "inprocess":
                    objOrderHeaderList = objOrderHeaderList.Where(u => u.OrderStatus == SD.StatusInProcess);
                    break;
                case "completed":
                    objOrderHeaderList = objOrderHeaderList.Where(u => u.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    objOrderHeaderList = objOrderHeaderList.Where(u => u.OrderStatus == SD.StatusApproved);
                    break;
                default:
                    break;
            }


            return Json(new { data = objOrderHeaderList });
        }
        #endregion
    }
}

