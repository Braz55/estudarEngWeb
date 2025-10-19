using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ficha2.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Ficha2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ficha2Context") ?? throw new InvalidOperationException("Connection string 'Ficha2Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();


//add service to the new class BdIniciatizer
builder.Services.AddTransient<DbInitializer>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services=scope.ServiceProvider;
var initializaer = services.GetRequiredService<DbInitializer>();

initializaer.run();


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
