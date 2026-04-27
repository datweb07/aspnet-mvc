using learn_asp.net_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace learn_asp.net_mvc.Controllers
{
    //[NonController]   // atttribute này sẽ hủy controller
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

        //public IActionResult Privacy()
        //{
        //    return View("Index", new TestModel() { Name = "Dat" });
        //}


        //[NonAction]     // atttribute này sẽ hủy action method
        public ActionResult Contact()
        {
            return View();
        }

        private void ValidateApiKey(string apiKey)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }
        }

        [HttpGet]
        [Route("api/users")]
        public IActionResult Users([FromServices] IUserRepository userRepository, [FromHeader] string apiKey)
        {
            _logger.LogInformation("[Users] method: {m}, api-key: {ak}", Request.Method, apiKey);

            ValidateApiKey(apiKey);

            return Content("Users: " + string.Join(',', userRepository.User) + " api-key: " + apiKey);
        }

        [HttpPost]
        [Route("api/users")]
        public IActionResult Users([FromServices] IUserRepository userRepository, [FromHeader] string apiKey, string user)
        {
            _logger.LogInformation("[Users] method: {m}, api-key: {ak}", Request.Method, apiKey);

            ValidateApiKey(apiKey);

            userRepository.Add(user);

            return Ok();
        }

        //[HttpPost]
        //public IActionResult Users(string user)
        //{
        //    _logger.LogInformation("[Users] method: {m}", Request.Method);

        //    return Content("Added user: " + user);
        //}

        public IActionResult Privacy()
        {
            return View();
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
