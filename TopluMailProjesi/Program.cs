using Microsoft.EntityFrameworkCore;
using TopluMailProjesi.Models;

var builder = WebApplication.CreateBuilder(args);
string conection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TMGContext>(options =>
{
    options.UseSqlServer(conection);
});
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
var app = builder.Build();

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
