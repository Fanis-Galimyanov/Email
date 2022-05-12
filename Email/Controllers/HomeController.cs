using Microsoft.AspNetCore.Mvc;

namespace Email.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Service service;

        public HomeController(ILogger<HomeController> logger, Service service)
        {
            _logger = logger;
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SendEmail()
        {
            service.SendEmail();
            return RedirectToAction("Index");
        }
    }
}
