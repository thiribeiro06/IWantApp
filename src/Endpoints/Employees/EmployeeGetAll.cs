using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        if (page == null || rows == null)
        {
            return Results.BadRequest("Valores de 'page' e/ou 'rows' são nulos. Por favor, forneça valores válidos.");
        }

        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}
