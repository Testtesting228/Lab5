using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Подключаем и страницы Razor Pages, и контроллеры, и локализацию
builder.Services.AddRazorPages().AddViewLocalization();
builder.Services.AddControllersWithViews().AddViewLocalization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("uk"),
        new CultureInfo("en")
    };
    options.DefaultRequestCulture = new RequestCulture("uk");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Включаем локализацию
app.UseRequestLocalization();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();
app.MapDefaultControllerRoute();

app.Run();