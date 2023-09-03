using iTalentBootcamp_Blog.Identity.ClaimProviders;
using iTalentBootcamp_Blog.Identity.Configurations;
using iTalentBootcamp_Blog.Identity.Extensions;
using iTalentBootcamp_Blog.Identity.Repository;
using iTalentBootcamp_Blog.Identity.Requirements;
using iTalentBootcamp_Blog.Identity.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.Configure<EmailConfigurations>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddIdentityWithOpt(builder.Configuration);//StartupExtension
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IClaimsTransformation, UserClaimProviders>();
builder.Services.AddScoped<IAuthorizationHandler, ExchangeExpireRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ViolenceRequirementHandler>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("GiresunPolicy", policy =>
    {
        policy.RequireClaim("city","Giresun");//(Claim-based Authorization) Herhangi bir business yok sadece varlığı kontrol ediliyor.
    });

    opt.AddPolicy("ExchangePolicy", policy =>
    {
        policy.AddRequirements(new ExchangeExpireRequirement());
    });

    opt.AddPolicy("ViolancePolicy", policy =>
    {
        policy.AddRequirements(new ViolenceRequirement(18));
    });
});
builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookieBuilder = new CookieBuilder();

    cookieBuilder.Name = "AuthCookie";
    opt.Cookie = cookieBuilder;
    opt.LoginPath = new PathString("/Auth/SignIn");
    opt.LogoutPath = new PathString("/Member/SignOut");
    opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true;//Expire süresi katlanır
});
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Member}/{action=Index}/{id?}");

app.Run();
