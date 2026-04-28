using learn_asp.net_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace learn_asp.net_mvc.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index(int id)
        {
            return View(new Collection { Id = id });
        }
    }
}
