using APIPedidos.Endpoints.Employees;
using Dapper;
using Microsoft.Data.SqlClient;

namespace APIPedidos.Data.Dapper.Employees;

public class GetAllUsers
{
    private readonly IConfiguration Configuration;

    public GetAllUsers(IConfiguration config)
    {
        this.Configuration = config;
    }

    public IEnumerable<EmployeeResponse> Execute(int pages, int rows)
    {
        SqlConnection conn = new SqlConnection(Configuration.GetConnectionString("Pedidos"));
        string query = @"SELECT Email,
                        ClaimValue as 'Name'
                        FROM AspNetUsers AS u
                        LEFT JOIN AspNetUserClaims
                        ON u.Id = UserId AND ClaimType = 'Name'
                        ORDER BY Name
                        OFFSET (@pages - 1) * @rows ROWS
                        FETCH NEXT @rows ROWS ONLY";
        return conn.Query<EmployeeResponse>(query, new { pages, rows });

    }
}
