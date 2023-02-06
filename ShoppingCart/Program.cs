using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ShoppingCartContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ShoppingCartContext)));

    options.UseLazyLoadingProxies();
});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Make the session cookie essential.
    options.Cookie.IsEssential = true;
});

// Configure api client.
builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5000");
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed data.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    {
        SeedData.Initialize(services);
    }
}


if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession(); // Enable sessions.


app.MapDefaultControllerRoute();


app.MapControllerRoute("test", "/test/{action}", new { controller = "Home" });

app.Run();