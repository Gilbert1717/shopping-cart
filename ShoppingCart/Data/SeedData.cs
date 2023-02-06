using Week6_Lectorial.Models;

namespace Week6_Lectorial.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<Week6LectorialContext>();

        // Look for stores.
        if(context.Stores.Any())
            return; // DB has already been seeded.

        // Stores.
        var melbourne = new Store { Name = "Melbourne" };
        var geelong = new Store { Name = "Geelong" };
        var preston = new Store { Name = "Preston" };

        // Products.
        var tv = new Product { Name = "TV", Price = 999.95m };
        var speakers = new Product { Name = "Speakers", Price = 500 };
        var entertainmentUnit = new Product { Name = "Entertainment Unit", Price = 220 };

        context.Stores.AddRange(melbourne, geelong, preston);
        context.Products.AddRange(tv, speakers, entertainmentUnit);
        context.StoreProducts.AddRange(
            new StoreProduct
            {
                Store = melbourne,
                Product = tv,
                Quantity = 10
            },
            new StoreProduct
            {
                Store = melbourne,
                Product = speakers,
                Quantity = 5
            },
            new StoreProduct
            {
                Store = melbourne,
                Product = entertainmentUnit,
                Quantity = 1
            },
            new StoreProduct
            {
                Store = geelong,
                Product = tv,
                Quantity = 2
            },
            new StoreProduct
            {
                Store = geelong,
                Product = speakers,
                Quantity = 3
            }
            // NOTE: The preston store intentionally has no products seeded.
        );

        context.SaveChanges();
    }
}
