using Microsoft.AspNetCore.Mvc;

namespace Agil.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Items()
        {
            return View();
        }
    }
}
