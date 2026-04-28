using learn_asp.net_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace learn_asp.net_mvc.Controllers
{
    public class HSKController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/p/{id}")]
        [Route("/certificate/{id}")]
        public IActionResult Certificate(string id)
        {
            return View(new HSK { id = id });
        }
    }
}
