﻿using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Template;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace IWantApp.Endpoints.Employees;

[Authorize(Policy = "EmployeePolicy")]
public class EmployeePost
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var newUser = new IdentityUser
        {
            UserName = employeeRequest.Name,
            Email = employeeRequest.Email,
        };

        var result = await userManager.CreateAsync(newUser, employeeRequest.Password);

        if(!result.Succeeded) 
        {
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        }

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId),
        };

        var claimResult = await userManager.AddClaimsAsync(newUser, userClaims);

        if(!claimResult.Succeeded) 
        {
            return Results.BadRequest(result.Errors.First());
        }

        

        return Results.Created($"/employee/{newUser.Id}", newUser.Id);
    }
}
