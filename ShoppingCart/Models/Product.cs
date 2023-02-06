using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Week6_Lectorial.Models;

public class Product
{
    public int ProductID { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [Column(TypeName = "decimal")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public virtual ICollection<StoreProduct> StoreProducts { get; set; }
}
