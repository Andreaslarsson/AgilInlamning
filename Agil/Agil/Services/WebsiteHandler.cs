using Agil.Data;
using Agil.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task SaveItem(User userId, Item itemId)
        {
            var user = await _ctx.Users.FirstAsync(u => u.Id == userId.Id);
            var item = await _ctx.Items.FirstAsync(i => i.Id == itemId.Id);
            user.SavedItems ??= new List<Item>();
            user.SavedItems.Add(item);
            await _ctx.SaveChangesAsync();
        }

        public List<Item> GetAllItems()
        {
            var itemList =  _ctx.Items
                .ToList();

            return itemList;
        }
        public User GetThisUser(string userId)
        {
            var thisUser = _ctx.Users
                .Include(a => a.Items)
                .Include(u => u.SavedItems)
                .First(a => a.Id == userId);

            return thisUser;
        }
        public async Task<List<Item>> MyPostedAdvertisement(string userId)
        {
            var thisUser = GetThisUser(userId);

            return thisUser.Items.ToList();
        }

        [HttpGet]
        public async Task<Item> GetSingelItem(int id)
        {
            var x = await _ctx.Items
                .FirstAsync(x => x.Id == id);
            return x;
        }
        public async Task<List<Item>> AllSavedItemsForUser(User user)
        {
            var getUser = await _ctx.Users
                .Include(x => x.SavedItems)
                .FirstAsync(x => x.Id == user.Id);

            var items = getUser.SavedItems.ToList();

            return items;
        }
    }
}
