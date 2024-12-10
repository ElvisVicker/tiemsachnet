


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class AdminRoleAttribute : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var vaitroClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (vaitroClaim == null || vaitroClaim.Value == "false")
        {
            context.Result = new ForbidResult(); 
        }
    }
}






