using Microsoft.EntityFrameworkCore;
using Week6_Lectorial.Models;

namespace Week6_Lectorial.Data;

public class Week6LectorialContext : DbContext
{
    public Week6LectorialContext(DbContextOptions<Week6LectorialContext> options) : base(options)
    { }

    public DbSet<Store> Stores { get; set; }
    public DbSet<StoreProduct> StoreProducts { get; set; }
    public DbSet<Product> Products { get; set; }

    // Fluent-API.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Setup composite primary key.
        builder.Entity<StoreProduct>().HasKey(x => new { x.StoreID, x.ProductID });
    }
}
