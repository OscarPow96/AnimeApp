using AnimeApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configuración de autorización global
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Middleware de redirección personalizado (debe ir después de UseAuthorization)
app.Use(async (context, next) =>
{
    var publicPaths = new[] {
        "/",
        "/Home/Index",
        "/Identity/Account/Login",
        "/Identity/Account/Register",
        "/Identity/Account/ForgotPassword",
        "/Identity/Account/ResetPassword"
    };

    if (!context.User.Identity.IsAuthenticated &&
        !publicPaths.Any(p => context.Request.Path.StartsWithSegments(p)))
    {
        context.Response.Redirect("/Identity/Account/Login?returnUrl=" + context.Request.Path);
        return;
    }

    await next();
});

// Configuración de rutas con excepciones
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configuración específica para páginas públicas
app.MapControllerRoute(
    name: "home",
    pattern: "Home/Index",
    defaults: new { controller = "Home", action = "Index" })
    .AllowAnonymous();

// Todas las páginas de Identity son públicas (login, register, etc.)
app.MapRazorPages()
    .AllowAnonymous();

// El resto de controladores requieren autenticación
app.MapControllers()
    .RequireAuthorization();

app.Run();