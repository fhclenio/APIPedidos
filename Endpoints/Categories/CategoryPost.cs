using APIPedidos.Data;
using APIPedidos.Model.Product;

namespace APIPedidos.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => InsertCategory;

    public static IResult InsertCategory(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category(categoryRequest.Name, "Clenio", "Clenio", categoryRequest.Active);

        if (!category.IsValid)
        {
            var errors = category.Notifications
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
            return Results.ValidationProblem(errors);
        }

        context.Category.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
