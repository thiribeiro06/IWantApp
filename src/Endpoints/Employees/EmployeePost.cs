using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest , UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser
        {
            UserName = employeeRequest.Name,
            Email = employeeRequest.Email,
        };

        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if(!result.Succeeded) 
        {
            return Results.BadRequest(result.Errors.First());
        }

        return Results.Created($"/employee/{user.Id}", user.Id);
    }
}
