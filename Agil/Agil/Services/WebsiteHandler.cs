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

        public List<Item> GetAllItems()
        {
            var itemList =  _ctx.Items
                .ToList();

            return itemList;
        }
    }
}
