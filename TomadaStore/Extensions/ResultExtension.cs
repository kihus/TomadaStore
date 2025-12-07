using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Result;

namespace TomadaStore.Models.Extensions;

public static class ResultExtension
{
    public static Result<CustomerResponseDto> ToResult(this List<CustomerResponseDto> dto, int page,
        int quantity, List<CustomerResponseDto> list, string url)
    {
        var info = new Info
        {
            CustomerQuantity = quantity,
            Pages = (quantity <= 20
                            ? 1
                            : (quantity / 20) + 1),
            Next = (list.Count > 20 ? $"{url}/customer/?page={page + 1}" : null),
            Previous = (page > 1 ? $"{url}/customer/?page={page - 1}" : null)
        };
        
        return new Result<CustomerResponseDto>(info, [.. list.Take(20)]);
    }
}
