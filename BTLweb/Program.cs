using BTLweb.Data;
using BTLweb.Repositories;
using BTLweb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

var builder = WebApplication.CreateBuilder(args);
// Thêm dịch vụ xác thực bằng cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";     // Trang đăng nhập
        options.LogoutPath = "/Header/Logout"; // Trang đăng xuất
    });

// Thêm dịch vụ Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Đăng ký repositories
builder.Services.AddScoped<ISignUpRepository, SignUpRepository>();
builder.Services.AddScoped(typeof(IGenericFunction<>), typeof(GenericFunction<>));

// Đăng ký services
builder.Services.AddScoped<IEmailSender, SmtEmailSender>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<ISignUpService, SignUpService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddControllersWithViews();
// Đăng ký DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot/image")),
    RequestPath = "/image"
});

app.UseRouting();
// Thêm middleware Session
app.UseSession();
app.UseAuthentication(); // thêm dòng này trước UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
