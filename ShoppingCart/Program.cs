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



builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed data.
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    {
        SeedData.Initialize(services);
    }
}


if(!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession(); // Enable sessions.



app.MapDefaultControllerRoute();


app.MapControllerRoute("test", "/test/{action}", new { controller = "Home" });

app.Run();
