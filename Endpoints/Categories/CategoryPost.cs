using APIPedidos.Data;
using APIPedidos.Model.Product;
using Microsoft.AspNetCore.Authorization;

namespace APIPedidos.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => InsertCategory;

    [Authorize]
    public static IResult InsertCategory(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category(categoryRequest.Name, "Clenio", "Clenio", categoryRequest.Active);

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        context.Category.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
