using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models;

public class OrderedProduct
{
    public int OrderID { get; set; }
    public virtual Order Order { get; set; }

    public int ProductID { get; set; }
    public virtual Product Product { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be an positive integer")]
    public int Quantity { get; set; }
}
