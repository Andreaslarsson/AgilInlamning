using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agil.Models
{
    public class ItemLocation
    {
        public List<Item>? Items { get; set; }
        public SelectList? Locations { get; set; }
        public string? SearchString { get; set; }
        public string? Location { get; set; }
    }
}
