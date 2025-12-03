namespace TomadaStore.Models.Entities;

public class Customer(
    string firstName,
    string lastName,
    string email
    )
{
    public int Id { get; private set; }
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public string Email { get; private set; } = email;
    public Status Status { get; set; } = Status.Active;
    public string? PhoneNumber { get; private set; }

    public Customer(
        string firstName,
        string lastName,
        string email,
        string? phoneNumber
        )
        : this(
              firstName,
              lastName,
              email
              )
    {
        PhoneNumber = phoneNumber;
    }
}
