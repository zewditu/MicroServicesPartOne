using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using Stripe;
using WebMvc.Models;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    //[Authorize]
    public class TicketOrderController : Controller
    {
        private readonly ITicketOrderService _orderSvc;
        //private readonly ILogger<TicketOrderController> _logger;
        private readonly IConfiguration _config;
        //private readonly IIdentityService<ApplicationUser> _identitySvc;


        public TicketOrderController(IConfiguration config,
            ILogger<TicketOrderController> logger,
            ITicketOrderService orderSvc
            //IIdentityService<ApplicationUser> identitySvc)
            )
        {
            _orderSvc = orderSvc;
            //_logger = logger;
            _config = config;
            //_identitySvc = identitySvc;
        }


        //public IActionResult Create(EventCatalogItem? fromEvent)
        //{
        //    //var user = _identitySvc.Get(HttpContext.User);
        //    ViewBag.StripePublishableKey = _config["StripePublicKey"];
        //    var order = new TicketOrder
        //    {
        //        OrderDate = DateTime.Now,
        //        OrderId = new Random().Next(),
        //        OrderStatus = OrderStatus.Preparing,
        //        OrderTotal = 20,
        //        FirstName = "",
        //        LastName = "",
        //        Address = "",
        //        BuyerId = "llddds",
        //        PaymentAuthCode = "80ljl090",
        //        TicketPrice = 20,
        //        EventName = "sdfsf",
        //        Quantity = 1,
        //        //TicketPrice = fromEvent.TicketPrice,
        //        //EventName = fromEvent.Name,
        //        //Quantity = fromEvent.TicketQuantity,
        //    };
        //    return View(order);
        //    //return RedirectToAction("Create", "TicketOrder");
        //}

        [HttpPost]
        public async Task<IActionResult> Create(EventCatalogItem fromEvent)
        {
            ViewBag.StripePublishableKey = _config["StripePublicKey"];

            var newOrder = new TicketOrder
            {
                OrderDate = DateTime.Now,
                OrderId = new Random().Next(),
                OrderStatus = OrderStatus.Preparing,
                OrderTotal = 20,
                FirstName = "",
                LastName = "",
                Address = "",
                BuyerId = "llddds",
                PaymentAuthCode = "80ljl090",
                TicketPrice = fromEvent.TicketPrice,
                EventName = fromEvent.Name,
                Quantity = fromEvent.TicketQuantity,

            };
            //return View(newOrder);


            //if (ModelState.IsValid)
            //{
            //var user = _identitySvc.Get(HttpContext.User);

            TicketOrder order = newOrder;

            //order.UserName = user.Email;
            //order.BuyerId = "";

            var options = new RequestOptions
            {
                ApiKey = _config["StripePrivateKey"]
            };
            var chargeOptions = new ChargeCreateOptions
            {
                //required
                Amount = (int)(order.OrderTotal * 100),
                Currency = "usd",
                Source = order.StripeToken,
                Customer = "cus_AFGbOSiITuJVDs",

            };

            var chargeService = new ChargeService();

            Charge stripeCharge = null;
            try
            {
                stripeCharge = chargeService.Create(chargeOptions, options);
                //_logger.LogDebug("Stripe charge object creation" + stripeCharge.StripeResponse.ToString());
            }
            catch (StripeException stripeException)
            {
                //_logger.LogDebug("Stripe exception " + stripeException.Message);
                ModelState.AddModelError(string.Empty, stripeException.Message);
                return View(newOrder);
            }

            try
            {
                if (stripeCharge.Id != null)
                {
                    //_logger.LogDebug("TransferID :" + stripeCharge.Id);
                    order.PaymentAuthCode = stripeCharge.Id;

                    //_logger.LogDebug("User {userName} started order processing", user.UserName);
                    int orderId = await _orderSvc.CreateTicketOrder(order);
                    //_logger.LogDebug("User {userName} finished order processing  of {orderId}.", order.UserName, order.OrderId);

                    return RedirectToAction("Complete", new { id = orderId, userName = "maddieijams@gmail.com" });
                }
                else
                {
                    ViewData["message"] = "Payment cannot be processed, try again";
                    return View(newOrder);
                }
            }
            catch (BrokenCircuitException)
            {
                ModelState.AddModelError("Error", "It was not possible to create a new order, please try later on. (Business Msg Due to Circuit-Breaker)");
                return View(newOrder);
            }
            //}
            //else
            //{
            //return View(newOrder);
            //}
        }

        public IActionResult Complete(int id, string userName = "maddieijams@gmail.com")
        {

            //_logger.LogInformation("User {userName} completed checkout on order {orderId}.", userName, id);
            return View(id);

        }
    }
}

//namespace WebMvc.Controllers
//{
//    //[Authorize]
//    public class TicketOrderController : Controller
//    {
//        private readonly ITicketOrderService _orderSvc;
//        //private readonly IIdentityService<ApplicationUser> _identitySvc;
//        private readonly ILogger<TicketOrderController> _logger;
//        private readonly IConfiguration _config;


//        public TicketOrderController(IConfiguration config,
//            ILogger<TicketOrderController> logger,
//            ITicketOrderService orderSvc
//            //IIdentityService<ApplicationUser> identitySvc)
//            )
//        {
//            //_identitySvc = identitySvc;
//            _orderSvc = orderSvc;
//            _logger = logger;
//            _config = config;
//        }

//        public IActionResult Create()
//        {
//            //var user = _identitySvc.Get(HttpContext.User);
//            ViewBag.StripePublishableKey = _config["StripePublicKey"];
//            var order = new TicketOrder
//            {
//                OrderDate = DateTime.Now,
//                OrderId = 987,
//                OrderStatus = OrderStatus.Preparing,
//                OrderTotal = 20,
//                FirstName = "",
//                LastName = "",
//                Address = "",
//                BuyerId = "",
//                PaymentAuthCode = "",
//                TicketPrice = 20,
//                EventName = ""
//            };
//            return View(order);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(TicketOrder frmOrder)
//        {

//            if (ModelState.IsValid)
//            {
//                //var user = _identitySvc.Get(HttpContext.User);

//                TicketOrder order = frmOrder;

//                //order.UserName = user.Email;
//                order.BuyerId = "";

//                var options = new RequestOptions
//                {
//                    ApiKey = _config["StripePrivateKey"]
//                };
//                var chargeOptions = new ChargeCreateOptions()

//                {
//                    //required
//                    Amount = (int)(order.OrderTotal * 100),
//                    Currency = "usd",
//                    Source = order.StripeToken,
//                    //optional
//                    Description = string.Format("Order Payment {0}", "fake"),
//                    ReceiptEmail = "maddieijams@gmail.com",

//                };

//                var chargeService = new ChargeService();



//                Charge stripeCharge = null;
//                try
//                {
//                    stripeCharge = chargeService.Create(chargeOptions, options);
//                    _logger.LogDebug("Stripe charge object creation" + stripeCharge.StripeResponse.ToString());
//                }
//                catch (StripeException stripeException)
//                {
//                    _logger.LogDebug("Stripe exception " + stripeException.Message);
//                    ModelState.AddModelError(string.Empty, stripeException.Message);
//                    return View(frmOrder);
//                }


//                try
//                {

//                    if (stripeCharge.Id != null)
//                    {
//                        //_logger.LogDebug("TransferID :" + stripeCharge.Id);
//                        order.PaymentAuthCode = stripeCharge.Id;

//                        //_logger.LogDebug("User {userName} started order processing", user.UserName);
//                        int orderId = await _orderSvc.CreateTicketOrder(order);
//                        //_logger.LogDebug("User {userName} finished order processing  of {orderId}.", order.UserName, order.OrderId);

//                        //await _cartSvc.ClearCart(user);
//                        return RedirectToAction("Complete", new { id = orderId, userName = "maddieijams@gmail.com" });
//                    }

//                    else
//                    {
//                        ViewData["message"] = "Payment cannot be processed, try again";
//                        return View(frmOrder);
//                    }

//                }
//                catch (BrokenCircuitException)
//                {
//                    ModelState.AddModelError("Error", "It was not possible to create a new order, please try later on. (Business Msg Due to Circuit-Breaker)");
//                    return View(frmOrder);
//                }
//            }
//            else
//            {
//                return View(frmOrder);
//            }
//        }

//        public IActionResult Complete(int id, string userName = "maddieijams@gmail.com")
//        {

//            _logger.LogInformation("User {userName} completed checkout on order {orderId}.", userName, id);
//            return View(id);

//        }
//    }
//}
