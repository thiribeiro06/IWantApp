using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        var query =
            @"SELECT AU.Email
                   , AC.ClaimValue AS Name
                FROM AspNetUsers AU
               INNER JOIN AspNetUserClaims AC
                  ON AU.Id = AC.UserId
                 AND AC.ClaimType = 'Name'
               ORDER BY Name
              OFFSET (@page - 1) * @rows ROWS
               FETCH NEXT  @rows 
                ROWS ONLY";

       return db.Query<EmployeeResponse>(
            query,
            new { page, rows }
            );
    }
}
