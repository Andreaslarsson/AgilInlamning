using Agil.Data;
using Agil.Models;
using Agil.Pages.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task AddItem(User user, int itemId, string title, string description, string category, int price, string place, DateTime createdDate)
        {
            _ctx.Items.Add(new Item()
            {
                Id = itemId,
                Title = title,
                Description = description,
                Place = place,
                Price = price,
                Category = category,
                CreatedDate = DateTime.Today,
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

        public IQueryable<Item> GetAllItems()
        {
            var itemList =  _ctx.Items;

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
                .Include(x=> x.User)
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

        public async Task RemoveSavedItem(User user, Item item)
        {
            var findItem = await _ctx.Items
                .Include(x => x.Savedby)
                .FirstAsync(x => x.Id == item.Id);

            var findUser = await _ctx.Users
                .Include(x => x.SavedItems)
                .FirstAsync(x => x.Id == user.Id);

            findItem.Savedby.Remove(findUser);

            findUser.SavedItems.Remove(findItem);

            await _ctx.SaveChangesAsync();
        }
        public async Task<ItemLocation> GetSearchedItems(string searchString, string location)
        {
            IQueryable<string> locationQuery = from i in _ctx.Items orderby i.Place select i.Place;

            var items = from i in _ctx.Items
                select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Title!.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(location))
            {
                items = items.Where(s => s.Place!.Contains(location));
            }
            var itemLocation = new ItemLocation()
            {
                Locations = new SelectList(await locationQuery.Distinct().ToListAsync()),
                Items = await items.ToListAsync()
            };

            return itemLocation;
        }
    }
}
