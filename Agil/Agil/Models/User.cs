using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Agil.Models;

public class User : IdentityUser
{
    [InverseProperty("User")]
    public ICollection<Item>? Items { get; set; }
    [InverseProperty("SavedBy")]
    public ICollection<Item>? SavedItems { get; set; }
}