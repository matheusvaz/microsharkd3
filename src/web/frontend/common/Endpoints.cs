using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Common
{
    public class Endpoints : Controller
    {
        [HttpGet("/signout")]
        public IActionResult SignOut(string sid, string iss)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentSid = User.FindFirst("sid")?.Value ?? "";

                if (string.Equals(currentSid, sid, StringComparison.Ordinal) &&
                    string.Equals(iss, Env.GetString("AUTH_HTTPS_URL"), StringComparison.Ordinal))
                {                    
                    Response.Cookies.Delete(Env.GetString("APP_SESSION_COOKIE_NAME"));
                }
            }

            return NoContent();
        }
    }
}
