namespace TomadaStore.Models.Models;

public class Category(
    string name, 
    string description
    )
{
    public string Id { get; private set; }
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;

}