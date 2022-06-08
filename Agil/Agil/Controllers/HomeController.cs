using Agil.Models;
using Agil.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agil.Controllers
{
    public class HomeController : Controller
    {
        private readonly Services.WebsiteHandler _websiteHandler;
        private readonly UserManager<User> _userManager;

        public HomeController(WebsiteHandler websiteHandler, UserManager<User> userManager)
        {
            _websiteHandler = websiteHandler;
            _userManager = userManager;
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
        public async Task<IActionResult> Confirm(int id)
        {
            var user = _websiteHandler.GetThisUser(_userManager.GetUserId(User));
            var i = _websiteHandler.GetSingelItem(id).Result;
            await _websiteHandler.SaveItem(user, i);

            return View(i);
        }
        public IActionResult Saved()
        {
            var user = _websiteHandler.GetThisUser(_userManager.GetUserId(User));
            var items = _websiteHandler.AllSavedItemsForUser(user).Result;

            return View(items);
        }
    }
}
