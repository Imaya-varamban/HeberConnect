using HeberConnect.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Add services to the container
// ----------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity + Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false) // confirm email not required
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

// ----------------------------
// Configure HTTP request pipeline
// ----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// ----------------------------
// Seed Users + Roles
// ----------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await EnsureRolesAndUsersAsync(services);
}

app.Run();

// ============================
// Helper methods
// ============================

async Task EnsureRolesAndUsersAsync(IServiceProvider services)
{
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // Create roles if not exist
    string[] roles = { "Admin", "Staff", "Student" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Create users with default passwords
    await EnsureUserAsync(userManager, "admin@heberconnect.com", "Admin@123", "Admin");
    await EnsureUserAsync(userManager, "staff1@heberconnect.com", "Staff@123", "Staff");
    await EnsureUserAsync(userManager, "staff2@heberconnect.com", "Staff@123", "Staff");
    await EnsureUserAsync(userManager, "student1@heberconnect.com", "Student@123", "Student");
    await EnsureUserAsync(userManager, "student2@heberconnect.com", "Student@123", "Student");
    await EnsureUserAsync(userManager, "student3@heberconnect.com", "Student@123", "Student");
}

async Task EnsureUserAsync(UserManager<IdentityUser> userManager,
    string email, string password, string role)
{
    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
    {
        user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
