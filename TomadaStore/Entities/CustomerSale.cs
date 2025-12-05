namespace TomadaStore.Models.Entities;

public class CustomerSale(
    int id,
    string firstName,
    string lastName
    )
{
    public int Id { get; private set; } = id;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
}
