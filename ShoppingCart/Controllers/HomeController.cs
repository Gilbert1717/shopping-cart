using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Week6_Lectorial.Data;
using Week6_Lectorial.ViewModels;

namespace Week6_Lectorial.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Week6LectorialContext _context;

    public HomeController(ILogger<HomeController> logger, Week6LectorialContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        // Using lazy loading.
        var stores = _context.Stores.OrderBy(x => x.Name).ToList();

        // Using eager loading.
        //var stores = _context.Stores.Include(x => x.StoreProducts).ThenInclude(x => x.Product).
        //    OrderBy(x => x.Name).ToList();

        return View(stores);
    }

    public IActionResult Store(int id) // Show: int storeID instead of int id.
    {
        // Using lazy loading.
        var store = _context.Stores.Find(id);

        // Using eager loading.
        //var store = _context.Stores.Include(x => x.StoreProducts).ThenInclude(x => x.Product).
        //    FirstOrDefault(x => x.StoreID == id);

        return View(store);
    }

    // Custom routing example, can also be used in combination with controllers.
    // Can also include custom variable names into the URL when routing.
    [Route("/PrivacyPage/{lat?}/{long?}")]
    public IActionResult Privacy(double lat, double @long)
    {
        _logger.LogInformation($"Lat: {lat}, Long: {@long}");

        return View();
    }

    // Logging example.
    public IActionResult Logging()
    {
        // See here for more information:
        // https://docs.microsoft.com/en-au/aspnet/core/fundamentals/logging/?view=aspnetcore-7.0
        _logger.LogTrace("Simple trace logging example.");
        _logger.LogDebug("Simple debug logging example.");
        _logger.LogInformation("Simple information logging example.");
        _logger.LogWarning("Simple warning logging example.");
        _logger.LogError("Simple error logging example.");
        _logger.LogCritical("Simple critical logging example.");

        // Purposely cause an exception and manually log the error.
        try
        {
            string s = null;
            int length = s.Length;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Unable to determine string length.");
        }

        // The following return View(); code will result in an unhandled exception as the view does not exists.
        // ReSharper disable once Mvc.ViewNotResolved
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
