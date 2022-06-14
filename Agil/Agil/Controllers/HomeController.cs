using Agil.Models;
using Agil.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Items(string searchString, string location, string sortOrder, string category)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
            }
            if (!string.IsNullOrEmpty(location))
            {
                ViewBag.Location = location;
            }

            if (!string.IsNullOrEmpty(category))
            {
                ViewBag.Category = category;
            }

            var items = _websiteHandler.GetSearchedItems(searchString, location, category);

            switch (sortOrder)
            {
                case "price_desc":
                    items = items.OrderByDescending(s => s.Price);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.Price);
                    break;
                case "date_desc":
                    items = items.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    items = items.OrderBy(s => s.CreatedDate);
                    break;
            }
            return View(items.ToList());
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
