using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ficha3.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ficha3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ficha3Context") ?? throw new InvalidOperationException("Connection string 'ficha3Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<DbInicializer>(); //Esta linha é o Registo. Estás a ensinar à aplicação como criar o teu DbInicializer

var app = builder.Build();

using var scope = app.Services.CreateScope();   //ent a crio a bolha apra correr o bdinicializer
var services = scope.ServiceProvider;           //aqui digo que o services é  provedor de servicos do scope

var inicializer = services.GetRequiredService<DbInicializer>();     //peco o servico a variavel services e guardo o dentro de outra varial
inicializer.Run();      //mando a variavel com o pedido do servico executar

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
