using Agil.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agil.Pages
{
    public class ResetDbModel : PageModel
    {
        private readonly DatabaseService _databaseService;
        private readonly IWebHostEnvironment _environment;

        public ResetDbModel(DatabaseService databaseService, IWebHostEnvironment environment)
        {
            _databaseService = databaseService;
            _environment = environment;
        }
        public IActionResult OnGet()
        {
            if (_environment.IsProduction()) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (_environment.IsProduction()) return NotFound();

            await _databaseService.RecreateAndSeed();
            return Page();
        }
    }
}
