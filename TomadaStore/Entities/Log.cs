using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Entities;

public class Log(
    string date, 
    string message, 
    string stackTrace, 
    string level, 
    DateTime createdAt
    )
{
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; private set; }
    public string Date { get; private set; } = date;
    public string Message { get; private set; } = message;
    public string StackTrace { get; private set; } = stackTrace;
    public string Level { get; private set; } = level;
    public DateTime CreatedAt { get; private set; } = createdAt;
}
