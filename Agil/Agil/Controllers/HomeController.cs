using Agil.Models;
using Agil.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agil.Controllers
{
    public class HomeController : Controller
    {
        private readonly Services.WebsiteHandler _websiteHandler;

        public HomeController(WebsiteHandler websiteHandler)
        {
            _websiteHandler = websiteHandler;
        }
        public async Task<IActionResult> Items(string searchString, string location)
        {
            var items = await _websiteHandler.GetSearchedItems(searchString, location);
            
            return View(items.Items);
        }
        public IActionResult MyPostedAdvertisement(string userId)
        {
            var myPostedAdvertisements = _websiteHandler.MyPostedAdvertisement(userId);
            return View(myPostedAdvertisements);
        }
    }
}
