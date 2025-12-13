using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ficha5.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ficha5Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ficha5Context") ?? throw new InvalidOperationException("Connection string 'ficha5Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
