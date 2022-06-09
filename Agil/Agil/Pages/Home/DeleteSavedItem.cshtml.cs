using Agil.Data;
using Agil.Models;
using Agil.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agil.Pages.Home
{
    public class DeleteSavedItemModel : PageModel
    {
        private readonly WebsiteHandler _websiteHandler;
        private readonly UserManager<User> _userManager;

        public DeleteSavedItemModel(WebsiteHandler websiteHandler, UserManager<User> userManager)
        {
            _websiteHandler = websiteHandler;
            _userManager = userManager;
        }

        [BindProperty]
        public Item Item { get; set; }
        public async Task OnGet(int id)
        {
            Item = await _websiteHandler.GetSingelItem(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var item = await _websiteHandler.GetSingelItem(id);
            var user =  _userManager.GetUserAsync(User).Result;

            if (item != null)
            {
                await _websiteHandler.RemoveSavedItem(user, item);
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Saved", "Home");
        }
    }
}
