using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using iTalentBootcamp_Blog.API.Filters;
using iTalentBootcamp_Blog.API.Modules;
using iTalentBootcamp_Blog.Core;
using iTalentBootcamp_Blog.Core.Dtos;
using iTalentBootcamp_Blog.Repository;
using iTalentBootcamp_Blog.Repository.UnitOfWork;
using iTalentBootcamp_Blog.Service.Extensions;
using iTalentBootcamp_Blog.Service.Mapping;
using iTalentBootcamp_Blog.Service.Middlewares;
using iTalentBootcamp_Blog.Service.Validations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateFilterAttribute());
    //options.Filters.Add<MerchantCodeActionFilter>();  //ActionFilter kullanılacaksa buradaki filtrelere eklenmeli
    //options.Filters.Add<MerchantCodeActionFilterAttribute>(); lFakat attributelerin burada tanıtılmasına gerek yok
});
//.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<PostCreateDtoValidator>());

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IValidator<PostCreateWithImageDto>, PostCreateDtoValidator>();
builder.Services.AddScoped<IValidator<PostUpdateDto>, PostUpdateDtoValidator>();

builder.Services.Configure<ApiBehaviorOptions>(config =>
{
    config.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddLogging(configuation =>
{
    configuation.ClearProviders()
    .AddDebug()
    .AddConsole();
});

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHello();//Custom Middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
