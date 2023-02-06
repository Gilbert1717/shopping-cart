using System.ComponentModel.DataAnnotations;

namespace Week6_Lectorial.Models;

public class Store
{
    public int StoreID { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    public virtual ICollection<StoreProduct> StoreProducts { get; set; }
}
