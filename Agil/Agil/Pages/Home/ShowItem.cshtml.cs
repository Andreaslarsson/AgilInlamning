using Agil.Models;
using Agil.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agil.Pages.Home
{
    public class ShowItemModel : PageModel
    {
        private readonly WebsiteHandler _websiteHandler;

        public ShowItemModel(WebsiteHandler websiteHandler, UserManager<User> userManager)
        {
            _websiteHandler = websiteHandler;
        }

        [BindProperty]
        public Item Item { get; set; }
        public async Task OnGet(int id)
        {
            Item = await _websiteHandler.GetSingelItem(id);
        }
    }
}
