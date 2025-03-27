using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Components;
using NeoRemiseria.Models;
using NeoRemiseria.Services;

// Registrar SweetAlert2
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Registro del servicio y configuracion para los modales. 
builder.Services.AddSweetAlert2(options => {
    options.DefaultOptions = new SweetAlertOptions { // Configuracion por defecto
        HeightAuto = false,
        AllowOutsideClick = false, // No cierra el modal al clickear fuera de él
        ShowCloseButton = true // Muestra el boton de cerrar en el titulo del modal
    };
}); 

// Registrar y configurar la conexión a la base de datos
var connection = builder.Configuration.GetConnectionString("DBRemiseria");
builder.Services.AddDbContext<DbremiseriaContext>(options =>
    options.UseMySql(connection,
        ServerVersion.AutoDetect(connection)));

// Registrar servicios
// Debe haber uno por cada tabla utilizada
builder.Services.AddScoped<ITable<Localidad>, LocalidadService>();
builder.Services.AddScoped<ITable<Marca>, MarcaService>();
builder.Services.AddScoped<ITable<Modelo>, ModeloService>();
builder.Services.AddScoped<ITable<Movil>, MovilService>();
builder.Services.AddScoped<ITable<Persona>, PersonaService>();
builder.Services.AddScoped<ITable<Provincia>, ProvinciaService>();
builder.Services.AddScoped<ITable<Telefono>, TelefonoService>();
builder.Services.AddScoped<ITable<Chofer>, ChoferService>();
builder.Services.AddScoped<ITable<Deuda>, DeudaService>();
builder.Services.AddScoped<ITable<Servicio>, ServicioService>();
builder.Services.AddScoped<CajaService>();
builder.Services.AddScoped<MovimientoService>();
builder.Services.AddScoped<PagoService>();
builder.Services.AddScoped<IVista<VModelo>, VModeloService>(); // No se usa

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
