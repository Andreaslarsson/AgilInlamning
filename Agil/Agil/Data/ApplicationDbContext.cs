using Agil.Models;
using Microsoft.EntityFrameworkCore;

namespace Agil.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Item> Items { get; set; }
}