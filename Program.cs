using Batch35_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using NToastNotify;

//STEP 1
var builder = WebApplication.CreateBuilder(args);

// NToastNotify
builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar = true,
    Timeout = 5000 //5 seconds
});

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure the Sql Server Database ConnectionStrings
builder.Services.AddDbContext<MvcEfContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("mvcConnection")));

//STEP 2
var app = builder.Build();

//set up the Middlewares and endpoints for our ASP.NET Core 
//app.MapGet("/", () => "Welcome to Core MVC:" + System.Diagnostics.Process.GetCurrentProcess().ProcessName);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// NToastNotify
app.UseNToastNotify();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

//STEP 3
app.Run();
