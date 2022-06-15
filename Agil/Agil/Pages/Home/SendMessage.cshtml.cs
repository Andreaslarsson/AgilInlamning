using Agil.Models;
using Agil.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agil.Pages.Home
{
    public class SendMessageModel : PageModel
    {
        private readonly WebsiteHandler _websiteHandler;
        private readonly UserManager<User> _userManager;

        public SendMessageModel(WebsiteHandler websiteHandler, UserManager<User> userManager)
        {
            _websiteHandler = websiteHandler;
            _userManager = userManager;
        }
        [BindProperty]
        public Message Message { get; set; }
        [BindProperty]
        public Item Item { get; set; }
        public async Task OnGet(int id)
        {
            Item = await _websiteHandler.GetSingelItem(id);
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var item = await _websiteHandler.GetSingelItem(id);

            await _websiteHandler.SendMessage(user, item, Message.Body, Message.MessageId);

            return RedirectToAction("Items", "Home");
        }
    }
}
