using Agil.Data;
using Agil.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Language;

namespace Agil.Services
{
    public class DatabaseService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<User> _userManager;

        public DatabaseService(ApplicationDbContext ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            var testUser = new User()
            {
                UserName = "test@mail.com",
                Email = "test@mail.com",
                Items = new List<Item>()
            };

            await _userManager.CreateAsync(testUser, "Passw0rd!");

            var itemList = new List<Item>
            {
                new Item()
                {
                    Title = "Soffa",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Category = "För hemmet",
                    Place = "Stockholm",
                    Price = 3000,
                    CreatedDate = DateTime.Now.AddDays(-5),
                    User = testUser
                },
                new Item()
                {
                    Title = "Iphone 11 Pro",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Category = "Elektronik",
                    Place = "Göteborg",
                    Price = 4000,
                    CreatedDate = DateTime.Now.AddDays(-20),
                    User = testUser
                },
            };

            await _ctx.AddRangeAsync(itemList);
            await _ctx.SaveChangesAsync();
        }

        public async Task Recreate()
        {
            await _ctx.Database.EnsureDeletedAsync();
            await _ctx.Database.EnsureCreatedAsync();
        }

        public async Task RecreateAndSeed()
        {
            await Recreate();
            await Seed();
        }

        public async Task CreateIfNotExist()
        {
            await _ctx.Database.EnsureCreatedAsync();
        }

        public async Task CreateAndSeedIfNotExist()
        {
            bool createdDatabase = await _ctx.Database.EnsureCreatedAsync();
            if (createdDatabase) await Seed();
        }
    }
}
