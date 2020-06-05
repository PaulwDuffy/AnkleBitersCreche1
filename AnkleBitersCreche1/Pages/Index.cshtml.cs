using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AnkleBitersCreche1.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnkleBitersCreche1.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            //Check if the user is logged in or not
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null) //if null no user is logged in so redirect to login
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            if (User.IsInRole(SD.AdminEndUser))
            {
                return RedirectToPage("/Users/Index");//if user is admin redirect to Users page.
            }
            return RedirectToPage("/Students/Index");
        }
    }
}
