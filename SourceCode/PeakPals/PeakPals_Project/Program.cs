using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.Data;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.DAL.Concrete;
using Microsoft.Extensions.DependencyInjection;
using PeakPals_Project.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebPWrecover.Services;
using PeakPals_Project.Controllers;

namespace PeakPals_Project;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options
                                    .UseSqlServer(connectionString)
                                    .UseLazyLoadingProxies());


        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<DbContext, ApplicationDbContext>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //repositories
        builder.Services.AddScoped<IFitnessDataEntryRepository, FitnessDataEntryRepository>();
        builder.Services.AddScoped<IClimberRepository, ClimberRepository>();

        //services
        builder.Services.AddScoped<IFitnessDataEntryService, FitnessDataEntryService>();
        builder.Services.AddScoped<IClimberService, ClimberService>();

        builder.Services.AddRazorPages();
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMigrationsEndPoint();
        }
        else
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

        app.MapControllerRoute(
            name: "profile",
            pattern: "profile/{username}",
            defaults: new { controller = "Profile", action = "GetProfile" });
        
        app.MapControllerRoute(
            name: "profile",
            pattern: "profile/edit",
            defaults: new { controller = "Profile", action = "EditProfile" });
        app.MapRazorPages();

        app.Run();

    }
}
