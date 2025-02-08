using Microsoft.AspNetCore.Identity;
using MilkMan.Shared.Constants;

namespace MilkMan.API.Middleware;

public class RoleInitializerMiddleware
{
    private readonly RequestDelegate _next;

    public RoleInitializerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in new[] { Roles.Admin, Roles.Customer, Roles.Driver })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        await _next(context);
    }
}

