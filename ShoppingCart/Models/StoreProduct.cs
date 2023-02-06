namespace Week6_Lectorial.Models;

public class StoreProduct
{
    public int StoreID { get; set; }
    public virtual Store Store { get; set; }

    public int ProductID { get; set; }
    public virtual Product Product { get; set; }

    public int Quantity { get; set; }
}
