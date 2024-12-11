//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//using Microsoft.AspNetCore.Authorization;
//namespace tiemsach.Attribute
//{
//    public class AdminRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
//    {
//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var userClaims = context.HttpContext.User.Claims;



//            var vaitroClaim = userClaims.FirstOrDefault(c => c.Type == "Vaitro");

//            if (vaitroClaim == null || vaitroClaim.Value != "true")
//            {
//                context.Result = new ForbidResult(); 
//            }
//        }
//    }
//}


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






