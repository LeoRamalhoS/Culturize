using Bulky.Utility;
using Culturize.DataAccess;
using Culturize.DataAccess.Repository.IRepository;
using Culturize.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using CulturizeWeb.Services;
using System;
using Culturize.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add MVC
builder.Services.AddControllersWithViews();

//Add razor pages for Identity views
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetSection("DefaultConnection").Value ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//Configure EF Core to connect to DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
        .AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

//Fix default routes for Identity **MUST BE AFTER AddIdentity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

//custom services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);
builder.Services.AddScoped<IDbInitializerService, DbInitializerService>();

var app = builder.Build();

//Run migrations and seed database
using (var scope = app.Services.CreateScope())
{
    //Migrations
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();

    //Initialize DB
    var dbInitializer = scope.ServiceProvider
        .GetRequiredService<IDbInitializerService>();

    await dbInitializer.SeedDataBase();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseExceptionHandler("/Error");
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
    pattern: "{area=App}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
