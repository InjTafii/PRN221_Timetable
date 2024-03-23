using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;
using Microsoft.AspNetCore.SignalR;
using TimeTable_Project.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddSession();
builder.Services.AddDbContext<ScheduleManagementContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddScoped<FileUploadService, FileUploadService>();
builder.Services.AddScoped<ImportFileService, ImportFileService>();
builder.Services.AddScoped<ValidationService, ValidationService>();
var app = builder.Build();
app.UseStaticFiles();
app.MapRazorPages();
app.UseSession();
app.Run();
