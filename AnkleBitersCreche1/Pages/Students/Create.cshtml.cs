using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AnkleBitersCreche.Data;
using AnkleBitersCreche.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace AnkleBitersCreche.Pages.Students
{
    [Authorize]
    public class CreateModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Student Student { get; set; }

        [TempData]

        public string StatusMessage { get; set; }


        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }




        public IActionResult OnGet(string userId = null)
        {
            Student = new Student();
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); //retrieve the user ID based on the user who has logged in
                userId = claim.Value;
            }
            Student.UserId = userId;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Student.Add(Student);
            await _db.SaveChangesAsync();

            StatusMessage = "Student Has been Added.";

            return RedirectToPage("Index", new { userId = Student.UserId });
        }
    }

}