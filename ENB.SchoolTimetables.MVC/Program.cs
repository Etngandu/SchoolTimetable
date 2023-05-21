using AspNetCoreHero.ToastNotification;
using AutoMapper;
using ENB.SchoolTimetables.EF;
using ENB.SchoolTimetables.EF.Repositories;
using ENB.SchoolTimetables.Entities;
using ENB.SchoolTimetables.Entities.Repositories;
using ENB.SchoolTimetables.Infrastructure;
using ENB.SchoolTimetables.MVC.Factory;
using ENB.SchoolTimetables.MVC.Help;
using ENB.SchoolTimetables.MVC.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SchoolTimetablesContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("SchoolTimetableCtr")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
               opt =>
               {
                   opt.Password.RequiredLength = 7;
                   opt.Password.RequireDigit = false;
                   opt.Password.RequireUppercase = false;
               })
                .AddEntityFrameworkStores<SchoolTimetablesContext>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsFactory>();
builder.Services.AddAutoMapper(typeof(SchoolTimetableProfile));
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IAsyncTeacherRepository, AsyncTeacherRepository>();
builder.Services.AddScoped<IAsyncSubjectRepository, AsyncSubjectRepository>();
//builder.Services.AddScoped<IAsyncPolicyTypeRepository, AsyncPolicyTypeRepository>();
//builder.Services.AddScoped<IAsyncClaimProcessingStageRepository, AsyncClaimProcessingStageRepository>();
builder.Services.AddScoped<IAsyncUnitOfWorkFactory, AsyncEFUnitOfWorkFactory>();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddScoped<IValidator<CreateAndEditTeacher>, CreateAndEditTeacherValidator>();
builder.Services.AddScoped<IValidator<CreateAndEditSubject>, CreateAndEditSubjectValidator>();


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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
