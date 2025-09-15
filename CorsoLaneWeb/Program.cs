using Microsoft.EntityFrameworkCore;
using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add DbContext to the services container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");





builder.Services.AddDefaultIdentity<user>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));





var app = builder.Build();


using (var scope = app.Services.CreateScope())
{

    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<user>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Add roles if they don't exist
        string[] roleNames = { "Admin", "Customer" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the roles and seed them to the database
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create a default admin user if one doesn't exist
        var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
        if (adminUser == null)
        {
            
            var newAdmin = new user
            {
                user_fullname = "ChanthanaAdmin",
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                AccessFailedCount = 0,
                // Set other required properties for your 'user' model
                EmailConfirmed = true // Bypass email confirmation for the admin user
            };

            // IMPORTANT: Replace "YourStrongPassword123!" with a secure password from configuration
            var result = await userManager.CreateAsync(newAdmin, "Admin@12345");

            if (result.Succeeded)
            {
                // Assign the 'Admin' role to the new user
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
