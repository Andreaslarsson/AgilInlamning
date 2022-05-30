using Agil.Data;
using Agil.Models;
using Microsoft.EntityFrameworkCore;

namespace Agil.Services
{
    public class WebsiteHandler
    {
        private readonly ApplicationDbContext _ctx;
        public WebsiteHandler(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddItem(User user, int itemId, string title, string description, string category, int price, string place)
        {
            _ctx.Items.Add(new Item()
            {
                Id = itemId,
                Title = title,
                Description = description,
                Place = place,
                Price = price,
                Category = category,
                User = user
            });

            await _ctx.SaveChangesAsync();
        }

        public async Task SaveItem(string userId, int itemId)
        {
            var user = await _ctx.Users.FirstAsync(u => u.Id == userId);
            var item = await _ctx.Items.FirstAsync(i => i.Id == itemId);
            user.SavedItems ??= new List<Item>();
            user.SavedItems.Add(item);
            await _ctx.SaveChangesAsync();
        }
    }
}
