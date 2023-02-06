using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models;

public class Order
{
    public int OrderID { get; set; }
    
    [Required]
    public DateTime OrderDate { get; init; }
    
    [Required]
    [StringLength(50)]
    [RegularExpression("([A-Z])[a-zA-Z\\s]*$",ErrorMessage="Name must start with an upper-case letter and only contain letters and spaces.")]
    public string CustomerName { get; set; }
    
    [Required]
    [StringLength(200)]
    public string DeliveryAddress { get; set; }
    
    public DateTime DeliveredDate { get; set; }

    public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
}
