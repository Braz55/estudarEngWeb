using Ficha2;
using Ficha2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext
builder.Services.AddDbContext<Ficha2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ficha2Context")
        ?? throw new InvalidOperationException("Connection string 'Ficha2Context' not found.")));

// Registra o DbInitializer
builder.Services.AddTransient<DbInitializer>();

// Adiciona MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Limpa dados antigos (opcional)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Ficha2Context>();
    TempClean.Run(context); // apaga os registros antigos
}

// Inicializa dados de teste
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var initializer = services.GetRequiredService<DbInitializer>();
    initializer.run(); // popula o banco com dados iniciais
}

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
