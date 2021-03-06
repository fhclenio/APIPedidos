using APIPedidos.Data;
using APIPedidos.Model.Product;

namespace APIPedidos.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => GetAllCategories;

    public static IResult GetAllCategories(ApplicationDbContext context)
    {
        var categories = context.Category.ToList();
        var response = categories.Select(c => new CategoryResponse { Name = c.Name, Active = c.Active, Id = c.Id });

        return Results.Ok(response);
    }
}
