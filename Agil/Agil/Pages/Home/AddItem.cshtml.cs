using Agil.Models;
using Agil.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agil.Pages.Website
{
    public class AddItemModel : PageModel
    {
        private readonly WebsiteHandler _websiteHandler;
        private readonly UserManager<User> _userManager;

        public AddItemModel(WebsiteHandler websiteHandler, UserManager<User> userManager)
        {
            _websiteHandler = websiteHandler;
            _userManager = userManager;
        }

        [BindProperty]
        public Item Item { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = _userManager.GetUserAsync(User).Result;

            await _websiteHandler.AddItem(user, Item.Id, Item.Title, Item.Description, Item.Category, Item.Price, Item.Place);
            
            return RedirectToPage("/Index");
        }
    }
}
