using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Agil.Data;
using Agil.Models;

namespace Agil.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly Agil.Data.ApplicationDbContext _context;

        public IndexModel(Agil.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Items != null)
            {
                Item = await _context.Items.ToListAsync();
            }
        }
    }
}
