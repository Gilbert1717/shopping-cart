using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers;

public class OrderController : Controller
{
    private readonly IHttpClientFactory _clientFactory;
    private HttpClient Client => _clientFactory.CreateClient("api");

    public OrderController(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

    public async Task<IActionResult> Index()
    {
        return View(await GetOrders());
    }

    private async Task<List<Order>> GetOrders()
    {
        var response = await Client.GetAsync("api/order");

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        // Storing the response details received from web api.
        var result = await response.Content.ReadAsStringAsync();

        // Deserializing the response received from web api and storing into a list.
        return JsonConvert.DeserializeObject<List<Order>>(result);
    }
}