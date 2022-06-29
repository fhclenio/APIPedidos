using APIPedidos.Data.Dapper.Employees;

namespace APIPedidos.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => GetAllEmployees;

    public static IResult GetAllEmployees(int? pages, int? rows, GetAllUsers query)
    {
        Dictionary<string, string[]> errors = Validate(pages, rows);
        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        IEnumerable<EmployeeResponse> employees = query.Execute(pages.Value, rows.Value);

        if (employees.Count() == 0)
            return Results.NotFound("There are no employees registered.");

        return Results.Ok(employees);
    }
    protected static Dictionary<string, string[]> Validate(int? pages, int? rows)
    {
        Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
        List<string> errorsList = new List<string>();
        if (pages == null)
        {
            errorsList.Add("Number of pages cannot be null.");
        }
        if (rows == null)
        {
            errorsList.Add("Number of rows cannot be null.");
        }
        if (rows > 10)
        {
            errorsList.Add("Number of rows cannot be bigger than 10.");
        }

        if (errorsList.Count > 0)
            errors.Add("Errors", errorsList.ToArray());
        return errors;
    }
}
