using ficha6.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ficha6Context>(options =>             //indica que vamos usar o contexto ficha6Context, visto que é ele que faz a ligação à base de dados, presisamos de registar pois nos que criamos o contexto
    options.UseSqlServer(builder.Configuration.GetConnectionString("ficha6Context")));

builder.Services.AddTransient<DbInitializer>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var initializer = services.GetRequiredService<DbInitializer>();
initializer.Run();   

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
    name : "filtered",      //rota personalizada que mapeia URLs do tipo "Filter/{letter?}" para a ação Index2 do StudentController
    pattern : "Filter/{letter?}",   //o parâmetro "letter" é opcional
    defaults: new { Controller = "Student", action = "Index2" }, //define o controlador e a ação padrão
    constraints: new { letter = @"^[A-Za-z]$" }); //adiciona uma restrição para que o parâmetro "letter" seja uma única letra (maiúscula ou minúscula)

app.MapControllerRoute(
    name: "ordered",      //rota personalizada que mapeia URLs do tipo "Order/{order?}" para a ação Index3 do StudentController
    pattern : "Order/{order?}",   //o parâmetro "order" é opcional
    defaults: new { Controller = "Student", action = "Index3" }, //define o controlador e a ação padrão
    constraints: new { order = @"^(ascendente|descendente)$" }); //adiciona uma restrição para que o parâmetro "order" seja "ascendente" ou "descendente"

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/* === CÁBULA RÁPIDA DE ROTAS (Program.cs) ===

app.MapControllerRoute(
    name: "nome_da_rota",
    pattern: "PrefixoUrl/{parametro?}",  // {parametro} tem de ser IGUAL ao do Controller!
    defaults: new { Controller = "NomeController", action = "NomeAction" },
    constraints: new { parametro = @"^(opcao1|opcao2)$" } // Validação (Opcional)
);

// REGEX ÚTEIS PARA CONSTRAINTS:
// @"^(ascendente|descendente)$"  -> Só aceita estas palavras exatas.
// @"^[a-zA-Z]+$"                 -> Só aceita letras.
// @"^\d+$"                       -> Só aceita números.

// PLANO B (EMERGÊNCIA): 
// Se der erro, apaga a linha 'constraints'. A rota funciona na mesma!
*/
