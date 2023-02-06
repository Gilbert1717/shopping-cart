using ShoppingCart.Models;

namespace ShoppingCart.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ShoppingCartContext>();

        // Look for stores.
        if(context.Orders.Any())
            return; // DB has already been seeded.

        // Orders.
        var john = new Order { OrderDate = DateTime.Now, CustomerName = "John", DeliveryAddress = "Melbourne", DeliveredDate = DateTime.Now + TimeSpan.FromDays(5)};
        var jeff = new Order { OrderDate = DateTime.Now, CustomerName = "Jeff", DeliveryAddress = "Geelong", DeliveredDate = DateTime.Now + TimeSpan.FromDays(7)};

        // Products.
        var tv = new Product { Name = "TV", Price = 999.95m };
        var speakers = new Product { Name = "Speakers", Price = 500 };
        var iMac = new Product { Name = "iMac", Price = 3000 };
        var iPhone = new Product { Name = "iPhone", Price = 2000 };

        context.Orders.AddRange(john, jeff);
        context.Products.AddRange(tv, speakers, iMac, iPhone);
        context.OrderedProducts.AddRange(
            new OrderedProduct
            {
                Order = john,
                Product = tv,
                Quantity = 2
            },
            new OrderedProduct
            {
                Order = john,
                Product = iMac,
                Quantity = 1
            },
            new OrderedProduct
            {
                Order = jeff,
                Product = tv,
                Quantity = 1
            },
            new OrderedProduct
            {
                Order = jeff,
                Product = speakers,
                Quantity = 1
            },
            new OrderedProduct
            {
                Order = jeff,
                Product = iMac,
                Quantity = 1
            },
            new OrderedProduct
            {
                Order = jeff,
                Product = iPhone,
                Quantity = 2
            }
            // NOTE: The preston store intentionally has no products seeded.
        );

        context.SaveChanges();
    }
}
