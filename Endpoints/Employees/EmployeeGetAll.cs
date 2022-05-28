using Dapper;
using Microsoft.Data.SqlClient;

namespace APIPedidos.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => GetAllEmployees;

    public static IResult GetAllEmployees(int? pages, int? rows, IConfiguration config)
    {
        Dictionary<string, string[]> errors = Validate(pages, rows);
        if(errors.Count > 0)
            return Results.ValidationProblem(errors);

        SqlConnection conn = new SqlConnection(config.GetConnectionString("Pedidos"));
        string query = @"SELECT Email,
                        ClaimValue as 'Name'
                        FROM AspNetUsers AS u
                        LEFT JOIN AspNetUserClaims
                        ON u.Id = UserId AND ClaimType = 'Name'
                        ORDER BY Name
                        OFFSET (@pages - 1) * @rows ROWS
                        FETCH NEXT @rows ROWS ONLY";
        IEnumerable<EmployeeResponse> employees = conn.Query<EmployeeResponse>(query, new { pages, rows });

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
