using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Components;
using NeoRemiseria.Models;
using NeoRemiseria.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar y configurar la conexión a la base de datos
var connection = builder.Configuration.GetConnectionString("DBRemiseria");
builder.Services.AddDbContext<DbremiseriaContext>(options =>
    options.UseMySql(connection,
        ServerVersion.AutoDetect(connection)));

// Registrar servicios
builder.Services.AddScoped<IMarca, MarcaService>();
builder.Services.AddScoped<IVista<VModelo>, VModeloService>();
builder.Services.AddScoped<ITable<Modelo>, ModeloService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
