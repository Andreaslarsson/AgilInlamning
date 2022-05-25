using Microsoft.AspNetCore.Identity;

namespace Agil.Models;

public class User : IdentityUser
{
    public ICollection<Item> Items { get; set; }
}