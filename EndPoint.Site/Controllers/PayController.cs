using Amir_Store.Application.Services.Carts;
using Amir_Store.Application.Services.Finances.Commands.AddRequestPay;
using Amir_Store.Application.Services.Finances.Queries.GetRequestPayService;
using Dto.Payment;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Amir_Store.Application.Services.Orders.Commands.AddNewOrder;
using ZarinPal.Class;
using ZarinPal.Interface;

namespace EndPoint.Site.Controllers
{
    [Authorize("Customer")]
    public class PayController : Controller
    {
        private readonly IAddRequestPayService _payService;
        private readonly ICartServices _cartServices;
        private readonly CookiesManager _cookiesManager;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private readonly IGetRequestPayService _getRequestPayService;
        private readonly IAddNewOrderService _addNewOrderService;
        public PayController(IAddRequestPayService payService, ICartServices cartServices,
            CookiesManager cookiesManager, IGetRequestPayService getRequestPayService,
            IAddNewOrderService addNewOrderService)
        {
            _payService = payService;
            _cartServices = cartServices;
            _cookiesManager = cookiesManager;
            _getRequestPayService = getRequestPayService;
            _addNewOrderService = addNewOrderService;

            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }
        public async Task<IActionResult> Index()
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartServices.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), UserId);
            if (cart.Data.SumAmount > 0)
            {
                var requestPay = _payService.Execute(cart.Data.SumAmount, UserId.Value);
                // ارسال به درگاه پرداخت
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09121112222",
                    CallbackUrl = $"https://localhost:44380/Pay/Verify?guid={requestPay.Data.guid}",
                    Description = "پرداخت فاکتور شماره :" + requestPay.Data.RequestPayId,
                    Email = requestPay.Data.Email,
                    Amount = requestPay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {
            if (status == "OK")
            {
                var requestPay = _getRequestPayService.Execute(guid);
                var verification = await _payment.Verification(new DtoVerification
                {
                    Amount = requestPay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                    Authority = authority
                }, Payment.Mode.sandbox);

                long? UserId = ClaimUtility.GetUserId(User);
                var cart = _cartServices.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), UserId);
                if (verification.Status == 100)
                {
                    _addNewOrderService.Execute(new RequestAddNewOrderServiceDto()
                    {
                        CartId = cart.Data.CartId,
                        UserId = UserId.Value,
                        RequestPayId = requestPay.Data.Id
                    });

                    //Redirect to orders
                    return RedirectToAction("Index", "Orders");
                }
                else
                {
                    return RedirectToAction("Index", "Orders");
                }

                //-------------------------
                //var verification2 = await _payment.Verification(new DtoVerification
                //{
                //    Amount = requestPay.Data.Amount,
                //    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                //    Authority = authority
                //}, Payment.Mode.sandbox);
                //if (verification2.Status == 100)
                //{

                //}
                //else
                //{

                //}
            }
            else
            {
                return RedirectToAction("Index", "Orders");
            }
            
            return View();
        }
    }
}
