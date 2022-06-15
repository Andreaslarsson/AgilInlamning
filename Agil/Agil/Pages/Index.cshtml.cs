using Agil.Models;
using Agil.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agil.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebsiteHandler _websiteHandler;

        public IndexModel(WebsiteHandler websiteHandler)
        {
            _websiteHandler = websiteHandler;
        }

        [BindProperty]
        public Item Item { get; set; }

        public void OnGet()
        {

        }
    }
}