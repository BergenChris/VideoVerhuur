using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VideoVerhuur.Filters
{
    public class KlantSessionAuthorizeAttribute : Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var klant = context.HttpContext.Session.GetString("Klant");

            if (string.IsNullOrEmpty(klant))
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
            }
        }
    }
}
