using Microsoft.EntityFrameworkCore;
using ShopMVCProject.Data;
using Microsoft.AspNetCore.Identity;
using ShopMVCProject.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});


// Konfiguracja autoryzacji JWT
builder.Services.AddAuthentication(options =>
{
    // Ustawienie domyœlnego schematu autoryzacji na JWT
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        // Ustawienia walidacji tokenu JWT
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Sprawdzanie wydawcy tokenu
            ValidateAudience = true, // Sprawdzanie odbiorcy tokenu
            ValidateLifetime = true, // Sprawdzanie wa¿noœci tokenu
            ValidateIssuerSigningKey = true, // Sprawdzanie klucza podpisu tokenu
            ValidIssuer = "https://localhost:7228", // Prawid³owy wydawca tokenu
            ValidAudience = "https://localhost:7228", // Prawid³owy odbiorca tokenu
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YcxjOMewdFfeZFQm5iGAYxTjR23Z93rLbyZucty3")) // Klucz u¿ywany do podpisywania tokenu
        };
    });

builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddScoped<IEmailSender, EmailSender>();

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
app.UseMiddleware<JwtMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Mapowanie endpointów na kontrolery
});
app.MapRazorPages();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
