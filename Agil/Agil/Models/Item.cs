namespace Agil.Models;

public class Item
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
    public string Place { get; set; }
    public DateTime? CreatedDate { get; set; }

    public virtual User? User { get; set; }
    public ICollection<User>? Savedby { get; set; }
}