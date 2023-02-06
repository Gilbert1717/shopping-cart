using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCart.Models;

namespace ShoppingCart.ViewModels;

public class ProductViewModel
{
    public List<OrderedProduct> OrderedProducts { get; set; }
    public SelectList productNameList { get; set; }
    public string productName { get; set; }
}
