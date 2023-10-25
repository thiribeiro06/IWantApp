using Dapper;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, IConfiguration configuration)
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

        var response = db.Query<EmployeeResponse>(
            query,
            new {page, rows}
            );

        return Results.Ok(response);
    }
}
