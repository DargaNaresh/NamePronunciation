using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace WebApp.ActionFilters
{

    public class ValidateLoginFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
           if( context.HttpContext.Session.GetString("EmpID")==null)
            {
                context.HttpContext.Response.Redirect("Login", true);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

}
