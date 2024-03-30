using Hospital.Business.Abstract;
using Hospital.Business.Concrete;
using Hospital.DataAccess.Abstract;
using Hospital.DataAccess.Concrete.EntityFramework;
using Hospital.Entities.Data;
using Hospital.WebUI.Abstract;
using Hospital.WebUI.Concrete;
using Hospital.WebUI.Hubs;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var context = builder.Configuration.GetConnectionString("mydb");

builder.Services.AddDbContext<CustomIdentityDbContext>(opt =>
{
    opt.UseSqlServer(context);
});

builder.Services.AddScoped<IDoctorDal, EFDoctorDal>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

builder.Services.AddScoped<IPatientDal, EFPatientDal>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IAppointmentDal, EFAppointmentDal>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<IAdminDal, EFAdminDal>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<IDepartmentDal, EFDepartmentDal>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IDataService, DataService>();

builder.Services.AddScoped<IPostDal, EFPostDal>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<IMediaService, MediaService>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddIdentity<CustomIdentityUser,CustomIdentityRole>()
    .AddEntityFrameworkStores<CustomIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();

//builder.Services.AddIdentity<Doctor, CustomIdentityRole>()
//    .AddEntityFrameworkStores<CustomIdentityDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddIdentity<Admin, CustomIdentityRole>()
//    .AddEntityFrameworkStores<CustomIdentityDbContext>()
//    .AddDefaultTokenProviders();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("Default", "{controller=Authentication}/{action=Start}/{id?}");
    endpoints.MapHub<UserHub>("/userhub");
});

app.Run();