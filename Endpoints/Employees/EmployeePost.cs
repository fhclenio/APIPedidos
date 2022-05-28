using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace APIPedidos.Endpoints.Employees;
public class EmployeePost
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => InsertEmployee;

    public static IResult InsertEmployee(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        IdentityUser user = new IdentityUser
        {
            UserName = (BuildUsername(employeeRequest.Name)),
            Email = employeeRequest.Email
        };

        IdentityResult result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        List<Claim> userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
        };

        IdentityResult claimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        if (!claimResult.Succeeded)
            return Results.ValidationProblem(claimResult.Errors.ConvertToProblemDetails());

        return Results.Created($"/employee/{user.Id}", user.Id);
    }
    protected static string BuildUsername(string name)
    {
        string userName = string.Empty;

        string[] nameSplit = name.Split(' ');
        for (int i = 0; i < nameSplit.Length; i++)
        {
            if (i != nameSplit.Length - 1)
                userName += nameSplit[i][0];
            else
                userName += nameSplit[i];
        }

        return userName.ToLower();
    }
}
