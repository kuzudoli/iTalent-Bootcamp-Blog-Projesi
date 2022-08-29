using iTalentBootcamp_Blog.Core.Repositories;
using iTalentBootcamp_Blog.Core;
using iTalentBootcamp_Blog.Repository.Repositories;
using iTalentBootcamp_Blog.Repository.UnitOfWork;
using iTalentBootcamp_Blog.Core.Services;
using iTalentBootcamp_Blog.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using iTalentBootcamp_Blog.Service.Services;
using iTalentBootcamp_Blog.Service.Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using iTalentBootcamp_Blog.API.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        //AppDbContext'in bulunduğu assembly alınıyor yani repository (tip güvenli olarak alındı)
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });

});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    containerBuilder.RegisterModule(new RepoServiceModule()));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/api/Auth/Login";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
