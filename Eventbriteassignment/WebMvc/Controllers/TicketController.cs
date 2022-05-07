using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IConfiguration _config;
        public TicketController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult Index()
        {
            ViewBag.StripePublishKey = _config["StripePublicKey"];
            return View();
        }
    }
}
