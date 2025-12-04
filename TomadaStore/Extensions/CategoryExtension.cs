using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.Entities;

namespace TomadaStore.Models.Extensions;

public static class CategoryExtension
{
    public static Category ToCategory(this CategoryRequestDto category)
    {
        return new Category(
            category.Name,
            category.Description
            );
    }
}
