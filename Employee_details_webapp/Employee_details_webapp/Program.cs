using System;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataAccess;
using RepositoryLayer.RepositoryPattern.Interfaces;
using RepositoryLayer.RepositoryPattern;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using FluentValidation.AspNetCore;
using Employee_details_webapp.Models.Validators;
using FluentValidation;
using Employee_details_webapp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidation(x =>
{
    x.DisableDataAnnotationsValidation = true;
    x.ImplicitlyValidateChildProperties = true;
    x.RegisterValidatorsFromAssemblyContaining<EmployeeValidator>();
});


builder.Services.AddDbContext<EmployeeDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("WebAppConnectionString")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped(typeof(IEmployeeJobHistoryRepository<>), typeof(EmployeeJobHistoryRepository<>));
builder.Services.AddTransient<IEmployeeJobHistoryService, EmployeeJobHistoryService>();

builder.Services.AddScoped(typeof(IEmployeeRepository<>), typeof(EmployeeRepository<>));
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

builder.Services.AddScoped(typeof(IPeopleRepository<>), typeof(PeopleRepository<>));
builder.Services.AddTransient<IPeopleService, PeopleService>();

builder.Services.AddScoped(typeof(IPositionRepository<>), typeof(PositionRepository<>));
builder.Services.AddTransient<IPositionService, PositionService>();

builder.Services.AddScoped<IValidator<AddViewModel>, EmployeeValidator>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
