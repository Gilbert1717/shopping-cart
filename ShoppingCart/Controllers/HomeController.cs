using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCart.Data;
using ShoppingCart.Models;
using ShoppingCart.ViewModels;

namespace ShoppingCart.Controllers;

public class HomeController: Controller
{
    private readonly ShoppingCartContext _context;

    public HomeController(ShoppingCartContext context)
    {
        _context = context;
    }
    
    // public IActionResult Index()
    // {
    //     var orders = _context.Orders.OrderBy(x => x.CustomerName).ToList();
    //     return View(orders);
    // }
    
    public IActionResult ProductDisplay(string productName)
    {
        var orderedProducts = _context.OrderedProducts.Select(x => x);
        var names = _context.OrderedProducts.Select(x => x.Product.Name).Distinct().OrderBy(x => x);
        
        if(!string.IsNullOrEmpty(productName))
            
            orderedProducts = orderedProducts.Where(x => x.Product.Name == productName);
        
        ProductViewModel productViewModel = new ProductViewModel()
        {
            OrderedProducts = orderedProducts.OrderBy(x => x.OrderID).ToList(),
            productNameList = new SelectList(names.ToList()),
        };
        return View(productViewModel);
    }
    
    
    public IActionResult Create([Bind("Name, Price")] Product product)
    {
        if(ModelState.IsValid)
        {
            _context.Add(product);
            _context.SaveChangesAsync();
            return RedirectToAction("ProductDisplay");
        }

        return View(product);
    }
    
    public IActionResult Index()
    {
        
        var products = _context.Products.OrderBy(x => x.ProductID).ToList();
        return View(products);
    }
    
    

    
    [Route("/PrivacyPage/{lat?}/{long?}")]
    public IActionResult Privacy(double lat, double @long)
    {

        return View();
    }
}