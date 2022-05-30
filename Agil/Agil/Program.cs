using Agil.Data;
using Agil.Models;
using Agil.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<WebsiteHandler>();
builder.Services.AddScoped<DatabaseService>();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("default"))
    );

builder.Services.AddDefaultIdentity<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider
        .GetRequiredService<DatabaseService>();

    if (app.Environment.IsProduction())
    {
        await ctx.CreateIfNotExist();
    }

    if (app.Environment.IsDevelopment())
    {
        await ctx.CreateAndSeedIfNotExist();
    }
}
app.UseAuthentication();
app.UseAuthorization();

app.Run();
