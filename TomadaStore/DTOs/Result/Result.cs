using TomadaStore.Models.DTOs.Customer;

namespace TomadaStore.Models.DTOs.Result;

public class Result<T>(
    Info info,
    List<T> results
    )
{
    public Info Info { get; private set; } = info;
    public List<T> Results { get; private set; } = results;
}
