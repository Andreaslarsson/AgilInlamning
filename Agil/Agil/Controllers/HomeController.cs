using Agil.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agil.Controllers
{
    public class HomeController : Controller
    {
        private readonly Services.WebsiteHandler _websiteHandler;

        public HomeController(WebsiteHandler websiteHandler)
        {
            _websiteHandler = websiteHandler;
        }
        public IActionResult Items()
        {
            return View();
        }
        public IActionResult MyPostedAdvertisement(string userId)
        {
            var myPostedAdvertisements = _websiteHandler.MyPostedAdvertisement(userId);
            return View(myPostedAdvertisements);
        }
    }
}
