namespace TomadaStore.Models.Models;

public class Product(
    string name, 
    string description, 
    decimal price, 
    Category category
    )
{
    public string Id { get; private set; }
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public decimal Price { get; private set; } = price;
    public Category Category { get; private set; } = category;
}
