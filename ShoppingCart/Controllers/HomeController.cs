using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCart.Data;
using ShoppingCart.Models;
using ShoppingCart.Utilities;
using ShoppingCart.ViewModels;

namespace ShoppingCart.Controllers;

public class HomeController: Controller
{
    private readonly ShoppingCartContext _context;

    public HomeController(ShoppingCartContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        
        var products = _context.Products.OrderBy(x => x.ProductID).ToList();
        return View(products);
    }
    
    
    public IActionResult OrderedProduct(string productName)
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
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create([Bind("Name, Price")] Product product)
    {
        if(product.Price.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(product.Price), "Amount cannot have more than 2 decimal places.");
        if(ModelState.IsValid)
        {
            _context.Add(product);
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(product);
    }
    
    
    
    

    
    [Route("/PrivacyPage/{lat?}/{long?}")]
    public IActionResult Privacy(double lat, double @long)
    {

        return View();
    }
}