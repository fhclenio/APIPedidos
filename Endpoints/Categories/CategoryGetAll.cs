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

        if (categories.Count == 0)
            return Results.NotFound("There are no categories registered.");

        var response = categories.Select(c => new CategoryResponse(c.Id, c.Name, c.Active));

        return Results.Ok(response);
    }
}
