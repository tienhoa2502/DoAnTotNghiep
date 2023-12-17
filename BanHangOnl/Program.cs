using Microsoft.AspNetCore.Authentication.Cookies;
using CodeMegaVNPay.Services;
using BanHangOnl.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/NotFound";
        options.LoginPath = "/DangNhap";
        options.LogoutPath = "/logout";
        options.Cookie.Name = "banhang_onl";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddSession(); 
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

app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
