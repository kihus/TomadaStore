namespace TomadaStore.Models.Models;

public class Venda(
    Customer customer, 
    List<Product> products, 
    decimal totalPrice
    )
{
    public string Id { get; private set; }
    public Customer Customer { get; private set; } = customer;
    public List<Product> Products { get; private set; } = products;
    public DateTime SaleDate { get; private set; } = DateTime.UtcNow.ToLocalTime();
    public decimal TotalPrice { get; private set; } = totalPrice;
}
