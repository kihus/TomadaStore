namespace TomadaStore.Models.Entities;

public class Sale(
    Customer customer, 
    List<ProductSale> products
    )
{
    public string Id { get; private set; }
    public Customer Customer { get; private set; } = customer;
    public List<ProductSale> Products { get; private set; } = products;
    public DateTime SaleDate { get; private set; } = DateTime.Now;
    public decimal TotalPrice { get; private set; } = products.Sum(x => x.TotalPrice);
}
