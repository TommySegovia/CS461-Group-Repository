using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.Data;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.DAL.Concrete;
using PeakPals_Project.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebPWrecover.Services;
using PeakPals_Project.Models;
using PeakPals_Project.Areas.Identity.Data;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.OpenApi.Models;
using PeakPals_Project;
using PeakPals_Project.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace PeakPals_Project;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // builder.Host.ConfigureLogging(logging =>
        // {
        //     logging.ClearProviders();
        //     logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
        //     logging.AddConsole();
        //     logging.AddDebug();
        // });

         string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
         var kvUri = "https://" + "peakpalsvaults" + ".vault.azure.net";
         var secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
         var connectionAppSecret = secretClient.GetSecret("AppConnectString");
         var connectionAuthSecret = secretClient.GetSecret("AuthConnectString");
         var sendGridApiKeySecret = secretClient.GetSecret("SendGridApiKey");

        // Add services to the container.
        //var connectionStringApp = builder.Configuration.GetConnectionString("PeakPalsAppDB") ?? throw new InvalidOperationException("Connection string 'PeakPalsAppDB' not found.");
        //var connectionStringAuth = builder.Configuration.GetConnectionString("PeakPalsAuthDB") ?? throw new InvalidOperationException("Connection string 'PeakPalsAuthDB' not found.");
        var connectionStringAuth = connectionAuthSecret.Value.Value;
        var connectionStringApp = connectionAppSecret.Value.Value;
        var SendGridKey = sendGridApiKeySecret.Value.Value;

        builder.Services.AddLogging(config =>
        {
            config.AddDebug();
            config.AddConsole();
            // You can add other built-in providers here
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options => options
                                    .UseSqlServer(connectionStringAuth)
                                    .UseLazyLoadingProxies());

        builder.Services.AddDbContext<PeakPalsContext>(options => options
                                        .UseSqlServer(connectionStringApp)
                                        .UseLazyLoadingProxies());

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddSwaggerGen(c => { c.CustomSchemaIds(type => type.FullName); c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });

        builder.Services.AddScoped<DbContext, PeakPalsContext>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //repositories
        builder.Services.AddScoped<IFitnessDataEntryRepository, FitnessDataEntryRepository>();
        builder.Services.AddScoped<IClimberRepository, ClimberRepository>();
        builder.Services.AddScoped<IClimbAttemptRepository, ClimbAttemptRepository>();
        builder.Services.AddScoped<ICommunityGroupRepository, CommunityGroupRepository>();
        builder.Services.AddScoped<IGroupListRepository, GroupListRepository>();
        builder.Services.AddScoped<IClimbTagEntryRepository, ClimbTagEntryRepository>();
        builder.Services.AddScoped<ICommunityMessageRepository, CommunityMessageRepository>();

        //services
        builder.Services.AddScoped<IFitnessDataEntryService, FitnessDataEntryService>();
        builder.Services.AddScoped<IClimberService, ClimberService>();

        builder.Services.AddRazorPages();
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.Configure<AuthMessageSenderOptions>(options => { options.SendGridKey = SendGridKey; });
        //builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

        builder.Services.AddScoped(sp => new GraphQLHttpClient("https://api.openbeta.io", new NewtonsoftJsonSerializer()));

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

        /* Re-enable these for continued development 
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
        */
        app.UseExceptionHandler("/Home/Error"); 
        app.UseHsts();

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
            pattern: "locations/areas/{id}",
            defaults: new { controller = "Locations", action = "Areas" });

        app.MapControllerRoute(
            name: "climb",
            pattern: "locations/climbs/{id}",
            defaults: new { controller = "Locations", action = "Climbs" });

        app.MapRazorPages();

        app.Run();

    }

   
}
