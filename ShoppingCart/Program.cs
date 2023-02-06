using Microsoft.EntityFrameworkCore;
using Week6_Lectorial.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog file logging support.
builder.Logging.AddFile(
    Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ShoppingCart-Log-{Date}.txt");

// Example of setting the minimum logging level (default is LogLevel.Information).
//builder.Logging.AddFile(
//    Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/ShoppingCart-Log-{Date}.txt", LogLevel.Trace);

// Add services to the container.
builder.Services.AddDbContext<Week6LectorialContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(Week6LectorialContext)));

    // Enable lazy loading.
    options.UseLazyLoadingProxies();
});

// Store session into Web-Server memory.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Make the session cookie essential.
    options.Cookie.IsEssential = true;
});

// Bonus Material: Store session into SQL Server.
// Please see session-commands.md file.
// Package required: Microsoft.Extensions.Caching.SqlServer
//builder.Services.AddDistributedSqlServerCache(options =>
//{
//    options.ConnectionString = builder.Configuration.GetConnectionString(nameof(Week6LectorialContext));
//    options.SchemaName = "dotnet";
//    options.TableName = "SessionCache";
//});
//builder.Services.AddSession(options => options.Cookie.IsEssential = true);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed data.
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services);
    }
    catch(Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession(); // Enable sessions.

// Custom controller route - the first route that is a match for the route is used.
//app.MapControllerRoute("test", "/test/{action}", new { controller = "Home" });

app.MapDefaultControllerRoute();

// Custom controller route - the default route would be used by default as it is comes first.
// NOTE: This custom route will still work if used.
// Example URL: /test/Index
app.MapControllerRoute("test", "/test/{action}", new { controller = "Home" });

app.Run();
