using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Entities;

public class CustomerSale(
    int id,
    string firstName,
    string lastName
    )
{
    [BsonElement("id")]
    public int Id { get; private set; } = id;

    [BsonElement("first_name")]
    public string FirstName { get; private set; } = firstName;

    [BsonElement("last_name")]
    public string LastName { get; private set; } = lastName;
}
