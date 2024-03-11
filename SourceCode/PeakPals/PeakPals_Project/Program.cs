using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.Data;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.DAL.Concrete;
using Microsoft.Extensions.DependencyInjection;
using PeakPals_Project.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebPWrecover.Services;
using System.Configuration;
using Microsoft.Identity.Client;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using PeakPals_Project.Models;
using PeakPals_Project.Controllers;
using PeakPals_Project.Areas.Identity.Data;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using PeakPals_Project;
namespace PeakPals_Project;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        //var connectionStringAuth = builder.Configuration.GetConnectionString("PeakPalsAuthDB") ?? throw new InvalidOperationException("Connection string 'PeakPalsAuthDB' not found.");
        //var connectionStringApp = builder.Configuration.GetConnectionString("PeakPalsAppDB") ?? throw new InvalidOperationException("Connection string 'PeakPalsAppDB' not found.");

        
        builder.Services.AddDbContext<ApplicationDbContext>(options => options
                                    //.UseSqlServer(connectionStringAuth)
                                    .UseSqlServer(connectionString)
                                    .UseLazyLoadingProxies());
        

        builder.Services.AddDbContext<PeakPalsContext>(options => options
                                //.UseSqlServer(connectionStringApp)
                                .UseSqlServer(connectionString) 
                                .UseLazyLoadingProxies());

        // builder.Configuration.AddAzureKeyVault(new Uri("https://peakpalsvault.vault.azure.net/"), new DefaultAzureCredential());

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<DbContext, PeakPalsContext>();
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

        builder.Services.AddScoped(sp => new GraphQLHttpClient("https://stg-api.openbeta.io", new NewtonsoftJsonSerializer()));

        builder.Services.AddScoped<IOpenBetaApiService, OpenBetaApiService>();


        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create the Admin role if it doesn't exist
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole("Admin");
                var result = roleManager.CreateAsync(role).Result;
            }

            // Assign the Admin role to a user
            //var user = userManager.FindByEmailAsync("test@email.com").Result;
            //if (user != null)
            //{
            //    var result = userManager.AddToRoleAsync(user, "Admin").Result;
            //}
        }

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
            pattern: "Profile/{username}",
            defaults: new { controller = "Profile", action = "GetProfile" });
        
        app.MapControllerRoute(
            name: "profile",
            pattern: "Profile/Edit",
            defaults: new { controller = "Profile", action = "EditProfile" });

        app.MapControllerRoute(
            name: "admin",
            pattern: "admin",
            defaults: new { controller = "Admin", action = "UserList" });
        
        app.MapControllerRoute(
            name: "area",
            pattern: "Locations/Areas/{id}",
            defaults: new { controller = "Locations", action = "Areas"});

        app.MapRazorPages();

        app.Run();

    }
}
