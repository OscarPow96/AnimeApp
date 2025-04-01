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

// Configuraci�n de autorizaci�n global
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

// Middleware de redirecci�n personalizado (debe ir despu�s de UseAuthorization)
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

// Configuraci�n de rutas con excepciones
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configuraci�n espec�fica para p�ginas p�blicas
app.MapControllerRoute(
    name: "home",
    pattern: "Home/Index",
    defaults: new { controller = "Home", action = "Index" })
    .AllowAnonymous();

// Todas las p�ginas de Identity son p�blicas (login, register, etc.)
app.MapRazorPages()
    .AllowAnonymous();

// El resto de controladores requieren autenticaci�n
app.MapControllers()
    .RequireAuthorization();

app.Run();