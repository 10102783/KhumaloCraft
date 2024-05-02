using KhumaloCraft.Data;
using Microsoft.AspNetCore.Builder; // Import the correct namespace for WebApplication
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Models;
using Microsoft.AspNetCore.Identity;
using KhumaloCraft.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductContext")));

builder.Services.AddIdentity<AppUser,IdentityRole>()
    .AddEntityFrameworkStores<ProductContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IRoleInitializer, RoleInitializer>();

builder.Services.AddScoped<ProductRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using(var scope = app.Services.CreateScope())
{
    var servicesProvider = scope.ServiceProvider;
    var roleInitializer = servicesProvider.GetRequiredService<IRoleInitializer>();
    roleInitializer.InitializeAsync().Wait();
}

app.Run();

