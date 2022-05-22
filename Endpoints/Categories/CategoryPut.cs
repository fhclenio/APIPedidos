using APIPedidos.Data;
using APIPedidos.Model.Product;
using Microsoft.AspNetCore.Mvc;

namespace APIPedidos.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => UpdateCategory;

    public static IResult UpdateCategory([FromRoute] Guid id ,CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = context.Category.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
            return Results.NotFound();

        category.EditInfo(categoryRequest.Name, categoryRequest.Active);
        category.EditedBy = "Clenio";
        category.EditedOn = DateTime.Now;

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        context.SaveChanges();

        return Results.Ok();
    }
}
