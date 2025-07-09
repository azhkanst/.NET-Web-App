using Microsoft.EntityFrameworkCore;
using Take_Home_Test___Web_App.Models; 



var builder = WebApplication.CreateBuilder(args);

// - retrieve connection string from appsetting.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// - This line registers your ApplicationDbContext with the Dependency Injection container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        sqlServerOptionsAction: sqlOptions => // Add this part for SQL Server specific options
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 2,           // Maximum number of retries (e.g., 5 times)
                maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
                errorNumbersToAdd: null);    // SQL error numbers to consider transient (null uses default)
        })); //

builder.Services.AddRazorPages(); // - Ensure this is also present to enable Razor Pages



// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapGet("/", context => {
    context.Response.Redirect("/Absen");
    return Task.CompletedTask;
});

app.MapRazorPages(); // - Ensure this maps your Razor Pages

app.Run();
