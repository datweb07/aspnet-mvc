using learn_asp.net_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace learn_asp.net_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;

        public HomeController(IRepository repository,ILogger<HomeController> logger)
        {
            this._repository = repository;
            _logger = logger;

            _logger.LogInformation("New");
        }

        public IActionResult Index()
        {
            return View(new TestModel() { Name = "Dat" });
        }

        public IActionResult Privacy()
        {
            return View("Index", new TestModel() { Name = "Dat" });
        }

        public IActionResult testActionMethod(string name, int n)
        {
            return Content("hello world " + _repository.GetById(name));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
