using Autofac.Extensions.DependencyInjection;
using Autofac;
using iTalentBootcamp_Blog.Web.Modules;
using iTalentBootcamp_Blog.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using iTalentBootcamp_Blog.Service.Mapping;
using iTalentBootcamp_Blog.Web.Services;
using Microsoft.Extensions.FileProviders;
using iTalentBootcamp_Blog.Web.Helpers;
using FluentValidation.AspNetCore;
using iTalentBootcamp_Blog.Service.Validations;
using FluentValidation;
using iTalentBootcamp_Blog.Core.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IValidator<PostCreateWithImageDto>, PostCreateDtoValidator>();
builder.Services.AddScoped<IValidator<PostUpdateDto>, PostUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
builder.Services.AddScoped<IPhotoHelper, PhotoHelper>();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        //AppDbContext'in bulunduğu assembly alınıyor yani repository (tip güvenli olarak alındı)
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Admin/Login";
    });

builder.Services.AddHttpClient<CategoryApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

builder.Services.AddHttpClient<PostApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

builder.Services.AddHttpClient<CommentApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

builder.Services.AddHttpClient<AuthApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

//builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
//    containerBuilder.RegisterModule(new RepoServiceModule()));

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

app.UseAuthentication();//for cookie auth
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "areas",
    areaName:"Admin",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
