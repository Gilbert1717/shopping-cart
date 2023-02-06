using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models;

public class Product
{
    public int ProductID { get; set; }

    [Required]
    [StringLength(50)]
    [RegularExpression("([A-Z])[a-zA-Z0-9\\s]*$",ErrorMessage="Name must start with an upper-case letter and only contain letters, numbers, and spaces.")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal")]
    [DataType(DataType.Currency)]
    [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public decimal Price { get; set; }
}
