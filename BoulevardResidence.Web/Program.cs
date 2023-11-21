using BoulevardResidence.Domain.Data;
using BoulevardResidence.Service;
using BoulevardResidence.Service.Helpers;
using BoulevardResidence.Service.Services;
using BoulevardResidence.Web.Utility;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.AddSupportedCultures(new string[] { "ru-RU", "en-US", "az-AZ" })
    .AddSupportedUICultures(new string[] { "ru-RU", "en-US", "az-AZ" });
});

builder.Services.AddLocalization();
//builder.Services.AddHostedService<DailyBackgroundService>();
//builder.Services.AddSingleton<DailyBackgroundService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddServiceLayer();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Aws"));
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    if (context.Request.Query.Count() > 0 &&
    context.Request.Query["culture"].ToString() != "")
    {
        System.Threading.Thread.CurrentThread.CurrentCulture =
         System.Threading.Thread.CurrentThread.CurrentUICulture
        = new CultureInfo(context.Request.Query["culture"].ToString());
        //save cuurrent culture in cookie
        context.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue
            (new RequestCulture(context.Request.Query["culture"].ToString()))
            , new CookieOptions() { Expires = DateTime.Now.AddYears(1) }
            );
    }

    await next.Invoke();
});

app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
