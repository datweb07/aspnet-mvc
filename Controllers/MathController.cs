using Microsoft.AspNetCore.Mvc;

namespace learn_asp.net_mvc.Controllers
{
    public class MathController : Controller
    {
        public IActionResult Sum(int x, int y)
        {
            int result = x + y;
            return Content("Sum: " + result.ToString());
        }
    }
}
    